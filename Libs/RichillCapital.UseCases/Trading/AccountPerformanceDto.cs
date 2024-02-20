using RichillCapital.Domain.Trading;

namespace RichillCapital.UseCases.Trading;

public sealed record AccountPerformanceDto()
{
    public static AccountPerformanceDto From(AccountPerformance accountPerformance) =>
        new();
}