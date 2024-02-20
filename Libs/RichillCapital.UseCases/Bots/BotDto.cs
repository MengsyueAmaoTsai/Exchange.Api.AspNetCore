using RichillCapital.Domain.Bots;

namespace RichillCapital.UseCases.Bots;

public sealed record BotDto(
    string Id,
    string Name,
    string Description,
    string Platform)
{
    internal static BotDto From(Bot bot) =>
        new(
            bot.Id.Value,
            bot.Name.Value,
            bot.Description.Value,
            bot.Platform.Name);
}