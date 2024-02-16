namespace RichillCapital.Exchange.Api.Endpoints.Accounts;

public sealed record OrderResponse(
    string Id,
    string TradeType,
    decimal Quantity,
    string Symbol,
    string OrderType,
    string TimeInForce,
    string Status);