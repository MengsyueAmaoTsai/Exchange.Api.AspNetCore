using RichillCapital.Domain.Trading;

namespace RichillCapital.UseCases.Trading;

public static class PositionExtensions
{
    public static PositionDto ToDto(this Position position) =>
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