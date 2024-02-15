namespace RichillCapital.UseCases.Trading;

public sealed record TradeDto(
    string Side,
    string Symbol,
    decimal Quantity,
    DateTimeOffset EntryTime,
    decimal EntryPrice,
    DateTimeOffset ExitTime,
    decimal ExitPrice,
    decimal Commission,
    decimal Tax,
    decimal Swap,
    string AccountId);
