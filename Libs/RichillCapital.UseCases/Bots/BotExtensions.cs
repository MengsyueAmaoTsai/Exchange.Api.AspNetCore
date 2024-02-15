using RichillCapital.Domain.Bots;

namespace RichillCapital.UseCases.Bots;

public static class BotExtensions
{
    public static BotDto ToDto(this Bot bot) =>
        new(
            bot.Id.Value,
            bot.Name.Value,
            bot.Description.Value,
            bot.Platform.Name);
}