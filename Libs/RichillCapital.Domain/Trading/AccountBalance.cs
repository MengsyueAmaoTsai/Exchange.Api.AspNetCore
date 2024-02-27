using RichillCapital.Domain.Shared;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Trading;

public sealed class AccountBalance : ValueObject
{
    private AccountBalance(
        Currency currency,
        decimal amount,
        AccountId accountId)
    {
        Currency = currency;
        Amount = amount;
        AccountId = accountId;
    }

    public Currency Currency { get; private set; }

    public decimal Amount { get; private set; }

    public AccountId AccountId { get; private set; }

    public static Result<AccountBalance> Create(
        Currency currency,
        decimal initialDeposit,
        AccountId accountId) =>
        initialDeposit.ToResult()
            .Ensure(AccountBalanceRules.NotNegative)
            .Map(amount => new AccountBalance(currency, amount, accountId));

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Currency;
        yield return Amount;
        yield return AccountId;
    }
}

internal static class AccountBalanceRules
{
    public static readonly (Func<decimal, bool> predicate, Error error) NotNegative = (
        amount => amount >= decimal.Zero,
        Error.Invalid("Account balance cannot be negative."));
}