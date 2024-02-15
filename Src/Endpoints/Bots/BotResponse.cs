namespace RichillCapital.Exchange.Api.Endpoints.Bots;

public sealed record BotResponse(
    string Id,
    string Name,
    string Description,
    string Platform);