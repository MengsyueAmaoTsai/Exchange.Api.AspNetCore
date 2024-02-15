using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monad;

namespace RichillCapital.Domain.Trading;

public sealed class AccountBalance : ValueObject
{
    private AccountBalance(
        Currency currency,
        decimal balance,
        AccountId accountId)
    {
        Currency = currency;
        Balance = balance;
        AccountId = accountId;
    }

    public Currency Currency { get; private set; }

    public decimal Balance { get; private set; }

    public AccountId AccountId { get; private set; }

    public static Result<AccountBalance> Create(
        Currency currency,
        decimal initialDeposit,
        AccountId accountId)
    {
        if (initialDeposit < 0)
        {
            return Error.Invalid("Initial deposit cannot be negative.");
        }

        return new AccountBalance(
            currency,
            initialDeposit,
            accountId);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Currency;
        yield return Balance;
        yield return AccountId;
    }
}