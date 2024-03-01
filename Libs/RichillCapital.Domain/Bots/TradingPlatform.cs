using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Bots;

public sealed class TradingPlatform : Enumeration<TradingPlatform>
{
    public static readonly TradingPlatform TradingView = new(nameof(TradingView), 1);
    public static readonly TradingPlatform MetaTrader5 = new(nameof(MetaTrader5), 2);
    public static readonly TradingPlatform CTrader = new(nameof(CTrader), 3);
    public static readonly TradingPlatform Quantower = new(nameof(Quantower), 4);
    public static readonly TradingPlatform MultiCharts = new(nameof(MultiCharts), 5);
    public static readonly TradingPlatform XQ = new(nameof(XQ), 6);
    public static readonly TradingPlatform TradeStation = new(nameof(TradeStation), 7);
    public static readonly TradingPlatform NinjaTrader = new(nameof(NinjaTrader), 8);
    public static readonly TradingPlatform QuantConnect = new(nameof(QuantConnect), 9);
    public static readonly TradingPlatform WealthLab = new(nameof(WealthLab), 10);
    public static readonly TradingPlatform MultiChartsNet = new("MultiCharts.Net", 11);

    private TradingPlatform(string name, int value)
        : base(name, value)
    {
    }
}