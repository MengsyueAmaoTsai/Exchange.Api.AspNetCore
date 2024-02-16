namespace RichillCapital.Exchange.Api.Endpoints.Accounts;

public sealed record PositionResponse(
    string Id,
    string Side,
    string Symbol,
    decimal Quantity,
    decimal Price,
    decimal Commission,
    decimal Tax,
    decimal Swap);