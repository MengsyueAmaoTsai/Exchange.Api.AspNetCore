namespace RichillCapital.Exchange.Api.Endpoints.Accounts;

public sealed record TradeResponse(
    string Side,
    string Symbol,
    decimal Quantity,
    DateTimeOffset EntryTime,
    decimal EntryPrice,
    DateTimeOffset ExitTime,
    decimal ExitPrice,
    decimal Commission,
    decimal Tax,
    decimal Swap);