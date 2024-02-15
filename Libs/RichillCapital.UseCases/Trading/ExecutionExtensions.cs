using RichillCapital.Domain.Trading;

namespace RichillCapital.UseCases.Trading;

public static class ExecutionExtensions
{
    public static ExecutionDto ToDto(this Execution execution) =>
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