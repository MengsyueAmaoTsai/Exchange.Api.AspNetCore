using RichillCapital.Domain.Trading;

namespace RichillCapital.UseCases.Trading;

public static class AccountExtensions
{
    public static AccountDto ToDto(this Account account) =>
        new(
            account.Id.Value,
            account.Name.Value,
            account.Currency.Name);
}