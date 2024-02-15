using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Bots;

public sealed class Bot : Entity<BotId>
{
    private Bot(BotId id)
        : base(id)
    {
    }

    public static Bot Create(BotId id)
    {
        var bot = new Bot(id);

        bot.RegisterDomainEvent(new BotCreatedDomainEvent(id));

        return bot;
    }
}