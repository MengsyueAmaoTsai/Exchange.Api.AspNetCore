using RichillCapital.Domain.Bots;

namespace RichillCapital.UseCases.Bots;

public sealed record SignalDto(
    DateTimeOffset Time,
    string TradeType,
    string Symbol,
    decimal Volume,
    decimal Price,
    string BotId)
{
    internal static SignalDto From(Signal signal) =>
        new(
            signal.Time,
            signal.TradeType.Name,
            signal.Symbol.Value,
            signal.Quantity,
            signal.Price,
            signal.BotId.Value);
}