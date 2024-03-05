using RichillCapital.Domain.Shared;
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
        NonEmptyDescription description,
        Side side,
        TradingPlatform platform)
        : base(id) =>
        (Name, Description, Side, Platform) = (name, description, side, platform);

    public BotName Name { get; private set; }

    public NonEmptyDescription Description { get; private set; }

    public Side Side { get; private set; }

    public TradingPlatform Platform { get; private set; }

    public IReadOnlyList<Signal> Signals => _signals.AsReadOnly();

    public static ErrorOr<Bot> Create(
        BotId id,
        BotName name,
        NonEmptyDescription description,
        Side side,
        TradingPlatform platform) => new Bot(
            id,
            name,
            description,
            side,
            platform)
            .ToErrorOr();

    public ErrorOr<Signal> EmitSignal(
        DateTimeOffset time,
        TradeType tradeType,
        Symbol symbol,
        decimal volume,
        decimal price) =>
        (time, tradeType, symbol, volume, price)
            .ToResult()
            .Then(tuple => Signal.Create(
                tuple.time,
                tuple.tradeType,
                tuple.symbol,
                tuple.volume,
                tuple.price,
                Id))
            .ToErrorOr();
}