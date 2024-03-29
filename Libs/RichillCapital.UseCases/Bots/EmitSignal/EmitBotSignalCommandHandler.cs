using RichillCapital.Domain;
using RichillCapital.Domain.Bots;
using RichillCapital.Domain.Common;
using RichillCapital.Domain.Shared;
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

        if (botId.IsFailure)
        {
            return botId.Error
                .ToErrorOr<BotId>();
        }

        var bot = await _botRepository.GetByIdAsync(botId.Value, cancellationToken);

        if (bot.IsNull)
        {
            return DomainErrors.Bots
                .NotFound(botId.Value)
                .ToErrorOr<BotId>();
        }

        var tradeType = TradeType.FromName(command.TradeType);

        if (tradeType.IsNull)
        {
            return Error
                .Invalid($"Invalid trade type {command.TradeType}")
                .ToErrorOr<BotId>();
        }

        var symbol = Symbol.From(command.Symbol);

        if (symbol.IsFailure)
        {
            return symbol.Error
                .ToErrorOr<BotId>();
        }

        var signal = bot.Value.EmitSignal(
            command.Time,
            tradeType.Value,
            symbol.Value,
            command.Volume,
            command.Price);

        if (signal.HasError)
        {
            return signal.Errors.ToErrorOr<BotId>();
        }

        _botRepository.Update(bot.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ErrorOr<BotId>.With(bot.Value.Id);
    }
}