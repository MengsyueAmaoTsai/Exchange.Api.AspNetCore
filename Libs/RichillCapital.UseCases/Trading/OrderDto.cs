namespace RichillCapital.UseCases.Trading;

public sealed record OrderDto(
    string Id,
    string TradeType,
    decimal Quantity,
    string Symbol,
    string Type,
    string TimeInForce,
    string Status,
    string AccountId);