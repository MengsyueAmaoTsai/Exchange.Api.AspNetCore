using RichillCapital.Domain.Shared;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Trading;

public sealed class AccountBalance : ValueObject
{
    public static readonly (Func<decimal, bool> predicate, Error error) NotNegative = (
        amount => amount >= decimal.Zero,
        Error.Invalid("Account balance cannot be negative."));

    private AccountBalance(
        Currency currency,
        decimal amount,
        AccountId accountId) =>
        (Currency, Amount, AccountId) = (currency, amount, accountId);

    public Currency Currency { get; private init; }

    public decimal Amount { get; private init; }

    public AccountId AccountId { get; private init; }

    public static Result<AccountBalance> Create(
        Currency currency,
        decimal initialDeposit,
        AccountId accountId)
    {
        // var validationResult = Result<decimal>
        //     .Ensure(initialDeposit, value => value > 0, Error.Invalid("Initial deposit cannot be negative."));

        // if (validationResult.IsFailure)
        // {
        //     return validationResult.Error
        //         .ToResult<AccountBalance>();
        // }

        return new AccountBalance(currency, initialDeposit, accountId).ToResult();
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Currency;
        yield return Amount;
        yield return AccountId;
    }
}
