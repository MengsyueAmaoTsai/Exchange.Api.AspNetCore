namespace RichillCapital.Exchange.Api.Endpoints.Accounts;

public sealed record OrderResponse(
    string Id,
    DateTimeOffset Time,
    string TradeType,
    decimal Quantity,
    decimal RemainingQuantity,
    string Symbol,
    string OrderType,
    string TimeInForce,
    string Status);