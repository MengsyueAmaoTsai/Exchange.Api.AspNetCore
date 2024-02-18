using RichillCapital.Domain.Trading;

namespace RichillCapital.UseCases.Trading;

public static class AccountPerformanceExtensions
{
    public static AccountPerformanceDto ToDto(this AccountPerformance accountPerformance)
    {
        return new AccountPerformanceDto();
    }
}