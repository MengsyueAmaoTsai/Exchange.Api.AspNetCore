using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Common;

namespace RichillCapital.Domain.Trading;

public sealed class OrderMatchingService(
    ILogger<OrderMatchingService> _logger,
    IRepository<Order> _orderRepository,
    IUnitOfWork _unitOfWork)
{
    public async Task MatchOrderAsync(
        Order order,
        CancellationToken cancellationToken = default)
    {
        var orderbook = GetHardCodeOrderBook();
        var entries = orderbook.GetOppositeEntries(order.TradeType);

        foreach (var entry in entries)
        {
            var entryQuantity = entry.Quantity;

            var executionQuantity = entry.CanSatisfyOrder(order)
                ? order.RemainingQuantity
                : entryQuantity;

            while (order.Status != OrderStatus.Executed)
            {
                var executionResult = order.Execute(
                               executionQuantity,
                               entry.Price,
                               0,
                               0);

                if (executionResult.IsError)
                {
                    return;
                }

                entryQuantity -= executionQuantity;

                if (entryQuantity == decimal.Zero)
                {
                    break;
                }
            }

            if (entryQuantity == decimal.Zero)
            {
                continue;
            }
        }

        _orderRepository.Update(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
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