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

    public static Result<AccountName> From(string name) => name
        .ToResult()
        .Ensure(
            value => !string.IsNullOrWhiteSpace(value),
            Error.Invalid("Account name cannot be empty."))
        .Ensure(
            value => value.Length <= MaxLength,
            Error.Invalid($"Account name cannot be longer than {MaxLength} characters."))
        .Then(value => new AccountName(value));
}