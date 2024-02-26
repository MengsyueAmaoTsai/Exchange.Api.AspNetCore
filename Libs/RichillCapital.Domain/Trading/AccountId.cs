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

    public static ErrorOr<AccountId> From(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return Error
                .Invalid("Account id cannot be empty.")
                .ToErrorOr<AccountId>();
        }

        if (id.Length > MaxLength)
        {
            return Error
                .Invalid($"Account id cannot be longer than {MaxLength} characters.")
                .ToErrorOr<AccountId>();
        }

        return new AccountId(id).ToErrorOr();
    }

    public static AccountId NewAccountId()
    {
        return From(Guid.NewGuid().ToString()).Value;
    }
}