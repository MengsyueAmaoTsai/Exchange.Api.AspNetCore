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
        var errorOrTime = time.ToErrorOr().Ensure(
            time => time <= DateTimeOffset.Now && time >= DateTimeOffset.Now.AddMinutes(-5),
            Error.Invalid("Time cannot be in the future."));

        if (errorOrTime.HasError)
        {
            return errorOrTime.Errors
                .ToErrorOr<Signal>();
        }

        var errorOrVolume = volume.ToErrorOr().Ensure(
            volume => volume > 0,
            Error.Invalid("Volume must be greater than zero."));

        if (errorOrVolume.HasError)
        {
            return errorOrVolume.Errors
                .ToErrorOr<Signal>();
        }

        var errorOrPrice = price.ToErrorOr().Ensure(
            price => price > 0,
            Error.Invalid("Price must be greater than zero."));

        if (errorOrPrice.HasError)
        {
            return errorOrPrice.Errors
                .ToErrorOr<Signal>();
        }

        return new Signal(
            time,
            tradeType,
            symbol,
            volume,
            price,
            botId).ToErrorOr();
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