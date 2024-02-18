using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Trading;

public sealed class TradeType : Enumeration<TradeType>
{
    public static readonly TradeType Buy = new TradeType(nameof(Buy), 1);
    public static readonly TradeType Sell = new TradeType(nameof(Sell), -1);

    private TradeType(string name, int value)
        : base(name, value)
    {
    }
}