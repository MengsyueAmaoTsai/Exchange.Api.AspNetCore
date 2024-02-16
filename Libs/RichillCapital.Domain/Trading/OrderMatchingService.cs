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
        var executionQuantity = order.RemainingQuantity;

        var executionResult = order.Execute(executionQuantity, 17000, 0, 0);

        if (executionResult.IsError)
        {
            _logger.LogError(
                "Failed to execute order {OrderId} {Status}. Reason: {Reason}",
                order.Id,
                order.Status.Name,
                executionResult.Error);

            return;
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