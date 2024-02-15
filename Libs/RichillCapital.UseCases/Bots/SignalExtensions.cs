using RichillCapital.Domain.Bots;

namespace RichillCapital.UseCases.Bots;

public static class SignalExtensions
{
    public static SignalDto ToDto(this Signal signal) =>
        new(
            signal.Time,
            signal.TradeType.Name,
            signal.Symbol.Value,
            signal.Quantity,
            signal.Price,
            signal.BotId.Value);
}