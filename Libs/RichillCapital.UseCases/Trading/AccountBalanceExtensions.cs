using RichillCapital.Domain.Trading;

namespace RichillCapital.UseCases.Trading;

public static class AccountBalanceExtensions
{
    public static AccountBalanceDto ToDto(this AccountBalance balance) =>
        new(
            balance.Currency.Name,
            balance.Amount, balance.
            AccountId.Value);
}