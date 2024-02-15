namespace RichillCapital.Exchange.Api.Endpoints.Accounts;

public sealed record AccountBalanceResponse(
    string Currency,
    decimal Amount);