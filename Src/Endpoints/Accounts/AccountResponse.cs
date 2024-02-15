namespace RichillCapital.Exchange.Api.Endpoints.Accounts;

public sealed record AccountResponse(
    string Id,
    string Name,
    string Currency);