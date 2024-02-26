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

    public static ErrorOr<AccountName> From(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Error
                .Invalid("Account name cannot be empty.")
                .ToErrorOr<AccountName>();
        }

        if (name.Length > MaxLength)
        {
            return Error
                .Invalid($"Account name cannot be longer than {MaxLength} characters.")
                .ToErrorOr<AccountName>();
        }

        return new AccountName(name).ToErrorOr();
    }
}