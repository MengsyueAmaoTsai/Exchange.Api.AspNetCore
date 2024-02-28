using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Trading;

public sealed class AccountId : SingleValueObject<string>
{
    public const int MaxLength = 36;

    public static readonly (Func<string, bool> predicate, Error error) IsNotEmpty = (
        id => !string.IsNullOrWhiteSpace(id),
        Error.Invalid("Account id cannot be empty."));

    internal static readonly (Func<string, bool> predicate, Error error) IsNotLongerThan = (
        id => id.Length <= AccountId.MaxLength,
        Error.Invalid($"Account id cannot be longer than {AccountId.MaxLength} characters."));

    private AccountId(string value)
        : base(value)
    {
    }

    public static Result<AccountId> From(string id) => Result<string>.Success(id)
        .Ensure(IsNotEmpty)
        .Ensure(IsNotLongerThan)
        .Then(value => new AccountId(value));

    public static AccountId NewAccountId() => From(Guid.NewGuid().ToString()).Value;
}


