namespace RichillCapital.UseCases.Bots;

public sealed record BotDto(
    string Id,
    string Name,
    string Description,
    string Platform);