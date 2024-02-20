using RichillCapital.Domain.Trading;

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
    string OrderId)
{
    internal static ExecutionDto From(Execution execution) =>
        new(
            execution.Time,
            execution.TradeType.Name,
            execution.Symbol.Value,
            execution.Quantity,
            execution.Price,
            execution.Commission,
            execution.Tax,
            execution.AccountId.Value,
            execution.OrderId.Value);
}