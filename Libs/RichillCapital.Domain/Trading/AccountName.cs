using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Trading;

public sealed class AccountName : SingleValueObject<string>
{
    public const int MaxLength = 100;

    private AccountName(string value)
        : base(value)
    {
    }

    public static ErrorOr<AccountName> From(string name) => ErrorOr<string>.Is(name)
        .Ensure(AccountNameRules.IsNotEmpty)
        .Ensure(AccountNameRules.IsNotLongerThan)
        .Map(value => new AccountName(value));
}

internal static class AccountNameRules
{
    public static readonly (Func<string, bool> predicate, Error error) IsNotEmpty = (
        id => !string.IsNullOrWhiteSpace(id),
        Error.Invalid("Account name cannot be empty."));

    internal static readonly (Func<string, bool> predicate, Error error) IsNotLongerThan = (
        id => id.Length <= AccountName.MaxLength,
        Error.Invalid($"Account name cannot be longer than {AccountName.MaxLength} characters."));
}