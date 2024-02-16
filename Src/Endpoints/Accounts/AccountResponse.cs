namespace RichillCapital.Exchange.Api.Endpoints.Accounts;

public record AccountResponse(
    string Id,
    string Name,
    string PositionMode,
    string Currency);