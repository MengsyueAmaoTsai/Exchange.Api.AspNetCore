using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Bots;

public sealed record class TradingPlatform : Enumeration<TradingPlatform>
{
    public static readonly TradingPlatform MetaTrader5 = new(nameof(MetaTrader5), 1);
    public static readonly TradingPlatform TradingView = new(nameof(TradingView), 2);

    private TradingPlatform(string name, int value)
        : base(name, value)
    {
    }
}