using RichillCapital.Domain.Trading;

namespace RichillCapital.UseCases.Trading;

public sealed record AccountBalanceDto(
    string Currency,
    decimal Amount,
    string AccountId)
{
    internal static AccountBalanceDto From(AccountBalance balance) =>
        new(
            balance.Currency.Name,
            balance.Amount, balance.
            AccountId.Value);
}