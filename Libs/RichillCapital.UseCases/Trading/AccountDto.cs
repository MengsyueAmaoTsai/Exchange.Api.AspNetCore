using RichillCapital.Domain.Trading;

namespace RichillCapital.UseCases.Trading;

public sealed record AccountDto(
    string Id,
    string Name,
    string PositionMode,
    string Currency,
    IEnumerable<AccountBalanceDto> Balance)
{
    internal static AccountDto From(Account account) =>
        new(
            account.Id.Value,
            account.Name.Value,
            account.PositionMode.Name,
            account.Currency.Name,
            account.Balances
                .Select(AccountBalanceDto.From));
}