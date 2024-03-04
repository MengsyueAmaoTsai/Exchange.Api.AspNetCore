using RichillCapital.Domain.Shared;
using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Bots;

public sealed class Signal : ValueObject
{
    private Signal(
        DateTimeOffset time,
        TradeType tradeType,
        Symbol symbol,
        decimal quantity,
        decimal price,
        BotId botId)
    {
        Time = time;
        TradeType = tradeType;
        Symbol = symbol;
        Quantity = quantity;
        Price = price;
        BotId = botId;
    }

    public DateTimeOffset Time { get; private set; }

    public TradeType TradeType { get; private set; }

    public Symbol Symbol { get; private set; }

    public decimal Quantity { get; private set; }

    public decimal Price { get; private set; }

    public BotId BotId { get; private set; }

    public static Result<Signal> Create(
        DateTimeOffset time,
        TradeType tradeType,
        Symbol symbol,
        decimal volume,
        decimal price,
        BotId botId) =>
        (time, tradeType, symbol, volume, price, botId)
            .ToResult()
            .Ensure(
                tuple => tuple.time <= DateTimeOffset.Now && tuple.time >= DateTimeOffset.Now.AddMinutes(-5),
                Error.Invalid("Time cannot be in the future."))
            .Ensure(
                tuple => tuple.volume > decimal.Zero,
                Error.Invalid("Volume must be greater than zero."))
            .Ensure(
                tuple => tuple.price > decimal.Zero,
                Error.Invalid("Price must be greater than zero."))
            .Then(tuple => new Signal(
                tuple.time,
                tuple.tradeType,
                tuple.symbol,
                tuple.volume,
                tuple.price,
                tuple.botId));

    public static ErrorOr<Signal> CreateHistory(
        DateTimeOffset time,
        TradeType tradeType,
        Symbol symbol,
        decimal volume,
        decimal price,
        BotId botId) =>
        (time, tradeType, symbol, volume, price, botId)
            .ToErrorOr()
            .Ensure(
                tuple => tuple.volume > decimal.Zero,
                Error.Invalid("Volume must be greater than zero."))
            .Ensure(
                tuple => tuple.price > decimal.Zero,
                Error.Invalid("Price must be greater than zero."))
            .Then(tuple => new Signal(
                tuple.time,
                tuple.tradeType,
                tuple.symbol,
                tuple.volume,
                tuple.price,
                tuple.botId));

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Time;
        yield return TradeType;
        yield return Symbol;
        yield return Quantity;
        yield return Price;
        yield return BotId;
    }
}

