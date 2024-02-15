namespace RichillCapital.UseCases.Trading;

public sealed record ExecutionDto(
    DateTimeOffset Time,
    string TradeType,
    string Symbol,
    decimal Quantity,
    decimal Price,
    decimal Commission,
    decimal Tax,
    string AccountId,
    string OrderId);