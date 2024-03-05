using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Trading;

public sealed class AccountId : SingleValueObject<string>
{
    public const int MaxLength = 36;

    private AccountId(string value)
        : base(value)
    {
    }

    public static Result<AccountId> From(string id) => id
        .ToResult()
        .Ensure(
            id => !string.IsNullOrWhiteSpace(id),
            Error.Invalid("Account id cannot be empty."))
        .Ensure(
            id => id.Length <= MaxLength,
            Error.Invalid($"Account id cannot be longer than {MaxLength} characters."))
        .Then(value => new AccountId(value));

    public static AccountId NewAccountId() => From(Guid.NewGuid().ToString()).Value;
}


