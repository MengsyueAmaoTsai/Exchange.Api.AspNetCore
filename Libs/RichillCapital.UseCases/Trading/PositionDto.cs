using RichillCapital.Domain.Trading;

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
    string AccountId)
{
    internal static PositionDto From(Position position) =>
        new(
            position.Id.Value,
            position.Symbol.Value,
            position.Side.Name,
            position.Quantity,
            position.Price,
            position.Commission,
            position.Tax,
            position.Swap,
            position.AccountId.Value);
}