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

    public static ErrorOr<Signal> Create(
        DateTimeOffset time,
        TradeType tradeType,
        Symbol symbol,
        decimal volume,
        decimal price,
        BotId botId)
    {
        if (volume <= decimal.Zero)
        {
            return Error.Invalid("Volume must be greater than 0.");
        }

        if (price <= decimal.Zero)
        {
            return Error.Invalid("Price must be greater than 0.");
        }

        return new Signal(
            time,
            tradeType,
            symbol,
            volume,
            price,
            botId);
    }

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