using RichillCapital.Domain.Bots;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Bots.EmitSignal;

public sealed record EmitBotSignalCommand(
    DateTimeOffset Time,
    string TradeType,
    string Symbol,
    decimal Volume,
    decimal Price,
    string BotId) :
    ICommand<ErrorOr<BotId>>;