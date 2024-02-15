using RichillCapital.Domain;
using RichillCapital.Domain.Bots;
using RichillCapital.Domain.Common;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monad;
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
        var id = BotId.From(command.BotId);

        if (id.IsError)
        {
            return id.Error;
        }

        var bot = await _botRepository.GetByIdAsync(id.Value, cancellationToken);

        if (bot.HasNoValue)
        {
            return DomainErrors.Bots.NotFound(id.Value);
        }

        var tradeType = TradeType.FromName(command.TradeType);

        if (tradeType.HasNoValue)
        {
            return Error.Invalid("Invalid trade type.");
        }

        var symbol = Symbol.From(command.Symbol);

        if (symbol.IsError)
        {
            return symbol.Error;
        }

        var errorOrSignal = bot.Value.EmitSignal(
            command.Time,
            tradeType.Value,
            symbol.Value,
            command.Volume,
            command.Price);

        if (errorOrSignal.IsError)
        {
            return errorOrSignal.Error;
        }

        _botRepository.Update(bot.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return bot.Value.Id;
    }
}