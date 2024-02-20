using RichillCapital.Domain.Trading;

namespace RichillCapital.UseCases.Trading;

public sealed record TradeDto(
    string Side,
    string Symbol,
    decimal Quantity,
    DateTimeOffset EntryTime,
    decimal EntryPrice,
    DateTimeOffset ExitTime,
    decimal ExitPrice,
    decimal Commission,
    decimal Tax,
    decimal Swap,
    string AccountId)
{
    internal static TradeDto From(Trade trade) =>
        new(
            trade.Side.Name,
            trade.Symbol.Value,
            trade.Quantity,
            trade.EntryTime,
            trade.EntryPrice,
            trade.ExitTime,
            trade.ExitPrice,
            trade.Commission,
            trade.Tax,
            trade.Swap,
            trade.AccountId.Value);
}
