namespace RichillCapital.UseCases.Bots;

public sealed record SignalDto(
    DateTimeOffset Time,
    string TradeType,
    string Symbol,
    decimal Volume,
    decimal Price,
    string BotId);