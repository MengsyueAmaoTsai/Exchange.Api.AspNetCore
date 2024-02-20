using RichillCapital.Domain.Trading;

namespace RichillCapital.UseCases.Trading;

public sealed record OrderDto(
    string Id,
    DateTimeOffset Time,
    string TradeType,
    decimal Quantity,
    decimal RemainingQuantity,
    string Symbol,
    string Type,
    string TimeInForce,
    string Status,
    string AccountId)
{
    internal static OrderDto From(Order order) =>
        new(
            order.Id.Value,
            order.Time,
            order.TradeType.Name,
            order.Quantity,
            order.RemainingQuantity,
            order.Symbol.Value,
            order.Type.Name,
            order.TimeInForce.Name,
            order.Status.Name,
            order.AccountId.Value);
}