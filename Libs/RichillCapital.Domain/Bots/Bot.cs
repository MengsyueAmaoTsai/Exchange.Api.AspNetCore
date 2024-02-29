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

    public static ErrorOr<Bot> Create(
        BotId id,
        BotName name,
        BotDescription description,
        TradingPlatform platform) =>
        new Bot(id, name, description, platform)
            .ToErrorOr()
            .Then(bot => bot.RegisterDomainEvent(new BotCreatedDomainEvent(id)));

    public ErrorOr<Signal> EmitSignal(
        DateTimeOffset time,
        TradeType tradeType,
        Symbol symbol,
        decimal volume,
        decimal price) =>
        Signal
            .Create(time, tradeType, symbol, volume, price, Id)
            .Then(signal =>
            {
                _signals.Add(signal);
                RegisterDomainEvent(new BotSignalEmittedDomainEvent(Id));
            });
}