
namespace RichillCapital.Domain.Trading;

public sealed class OrderMatchingService()
{
    public async Task MatchOrderAsync(Order order)
    {
        var orderbook = GetHardCodeOrderBook();

        // Only support market order with IOC
    }

    private static OrderBook GetHardCodeOrderBook() =>
        OrderBook.Create(
            [
                OrderBookEntry.Create(17100, 10).Value,
                OrderBookEntry.Create(17099, 20).Value,
                OrderBookEntry.Create(17098, 30).Value
            ],
            [
                OrderBookEntry.Create(17101, 10).Value,
                OrderBookEntry.Create(17102, 20).Value,
                OrderBookEntry.Create(17103, 30).Value
            ]).Value;
}