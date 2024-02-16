namespace RichillCapital.Exchange.Api.Endpoints.Accounts;

public sealed record ExecutionResponse(
    DateTimeOffset Time,
    string TradeType,
    string Symbol,
    decimal Quantity,
    decimal Price,
    decimal Commission,
    decimal Tax);