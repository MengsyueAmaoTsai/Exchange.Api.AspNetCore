namespace RichillCapital.Exchange.Api.Endpoints.Accounts;

public sealed record AccountWithBalancesResponse(
    string Id,
    string Name,
    string Currency,
    IEnumerable<AccountBalanceResponse> Balances) :
    AccountResponse(Id, Name, Currency);