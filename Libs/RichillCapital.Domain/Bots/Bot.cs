using RichillCapital.Domain.Bots.Events;
using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Bots;

public sealed class Bot : Entity<BotId>
{
    private static readonly IReadOnlyList<TradingPlatform> SupportedPlatforms = [.. TradingPlatform.Members];

    private readonly List<Signal> _signals = [];

    private Bot(
        BotId id,
        BotName name,
        BotDescription description,
        Side side,
        TradingPlatform platform)
        : base(id) =>
        (Name, Description, Side, Platform) = (name, description, side, platform);

    public BotName Name { get; private set; }

    public BotDescription Description { get; private set; }

    public Side Side { get; private set; }

    public TradingPlatform Platform { get; private set; }

    public IReadOnlyList<Signal> Signals => _signals.AsReadOnly();

    public static ErrorOr<Bot> Create(
        BotId id,
        BotName name,
        BotDescription description,
        Side side,
        TradingPlatform platform) =>
        ErrorOr<TradingPlatform>
            .Ensure(platform, IsSupported, BotErrors.TradingPlatformNotSupported)
            .Then(() => new Bot(
                id,
                name,
                description,
                side,
                platform));

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

    private static bool IsSupported(TradingPlatform platform) =>
        SupportedPlatforms.Contains(platform);
}