using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monad;

namespace RichillCapital.Domain.Trading;

public sealed class AccountId : SingleValueObject<string>
{
    public const int MaxLength = 36;

    private AccountId(string value)
        : base(value)
    {
    }

    public static ErrorOr<AccountId> From(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return Error.Invalid("Account id cannot be empty.");
        }

        if (id.Length > MaxLength)
        {
            return Error.Invalid(
                $"Account id cannot be longer than {MaxLength} characters.");
        }

        return new AccountId(id);
    }

    public static AccountId NewAccountId()
    {
        return From(Guid.NewGuid().ToString()).Value;
    }
}