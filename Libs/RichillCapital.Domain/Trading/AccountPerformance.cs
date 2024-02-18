using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Trading;

public sealed class AccountPerformance : ValueObject
{
    private AccountPerformance()
    {
    }

    public static AccountPerformance GenerateFromClosedTrades(IEnumerable<Trade> trades)
    {
        var performance = new AccountPerformance();

        return performance;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        throw new NotImplementedException();
    }
}