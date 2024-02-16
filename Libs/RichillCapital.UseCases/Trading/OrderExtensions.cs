using RichillCapital.Domain.Trading;

namespace RichillCapital.UseCases.Trading;

public static class OrderExtensions
{
    public static OrderDto ToDto(this Order order) =>
        new(
            order.Id.Value,
            order.TradeType.Name,
            order.Quantity,
            order.RemainingQuantity,
            order.Symbol.Value,
            order.Type.Name,
            order.TimeInForce.Name,
            order.Status.Name,
            order.AccountId.Value);
}