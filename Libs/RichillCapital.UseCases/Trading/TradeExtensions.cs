using RichillCapital.Domain.Trading;

namespace RichillCapital.UseCases.Trading;

public static class TradeExtensions
{
    public static TradeDto ToDto(this Trade trade) =>
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