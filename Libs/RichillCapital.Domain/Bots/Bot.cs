using RichillCapital.Domain.Bots.Events;
using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Bots;

public sealed class Bot : Entity<BotId>
{
    private readonly List<Signal> _signals = [];

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

    public ErrorOr<Signal> EmitSignal(
        DateTimeOffset time,
        TradeType tradeType,
        Symbol symbol,
        decimal volume,
        decimal price)
    {
        var signal = Signal.Create(
            time,
            tradeType,
            symbol,
            volume,
            price,
            Id);

        if (signal.IsError)
        {
            return signal.Errors.ToList();
        }

        _signals.Add(signal.Value);

        RegisterDomainEvent(new BotSignalEmittedDomainEvent(Id));

        return signal;
    }
}