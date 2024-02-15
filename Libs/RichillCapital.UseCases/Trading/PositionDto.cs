namespace RichillCapital.UseCases.Trading;

public sealed record PositionDto(
    string Id,
    string Side,
    string Symbol,
    decimal Quantity,
    decimal Price,
    decimal Commission,
    decimal Tax,
    decimal Swap,
    string AccountId);