using RichillCapital.Domain;
using RichillCapital.Domain.Bots;
using RichillCapital.Domain.Common;
using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Bots.EmitSignal;

internal sealed class EmitBotSignalCommandHandler(
    IRepository<Bot> _botRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<EmitBotSignalCommand, ErrorOr<BotId>>
{
    public async Task<ErrorOr<BotId>> Handle(
        EmitBotSignalCommand command,
        CancellationToken cancellationToken)
    {
        var botId = BotId.From(command.BotId);

        if (botId.IsError)
        {
            return botId.Errors.ToList();
        }

        var bot = await _botRepository.GetByIdAsync(botId.Value, cancellationToken);

        if (bot.HasNoValue)
        {
            return DomainErrors.Bots.NotFound(botId.Value);
        }

        var tradeType = TradeType.FromName(command.TradeType);

        if (tradeType.HasNoValue)
        {
            return Error.Invalid($"Invalid trade type {command.TradeType}");
        }

        var symbol = Symbol.From(command.Symbol);

        if (symbol.IsError)
        {
            return symbol.Errors.ToList();
        }

        var signal = bot.Value.EmitSignal(
            command.Time,
            tradeType.Value,
            symbol.Value,
            command.Volume,
            command.Price);

        if (signal.IsError)
        {
            return signal.Errors.ToList();
        }

        _botRepository.Update(bot.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return bot.Value.Id;
    }
}