using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Bots;

public sealed class Bot : Entity<BotId>
{
    private readonly List<Signal> _signals = new();

    private Bot(
        BotId id,
        BotName name,
        BotDescription description,
        TradingPlatform platform)
        : base(id)
    {
        Name = name;
        Description = description;
        Platform = platform;
    }

    public BotName Name { get; private set; }

    public BotDescription Description { get; private set; }

    public TradingPlatform Platform { get; private set; }

    public IReadOnlyList<Signal> Signals => _signals.AsReadOnly();

    public static Bot Create(
        BotId id,
        BotName name,
        BotDescription description,
        TradingPlatform platform)
    {
        var bot = new Bot(id, name, description, platform);

        bot.RegisterDomainEvent(new BotCreatedDomainEvent(id));

        return bot;
    }
}