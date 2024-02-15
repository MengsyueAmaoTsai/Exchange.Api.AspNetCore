namespace RichillCapital.Exchange.Api.Endpoints.Bots;

public sealed record SignalResponse(
    DateTimeOffset Time,
    string TradeType,
    string Symbol,
    decimal Volume,
    decimal Price);