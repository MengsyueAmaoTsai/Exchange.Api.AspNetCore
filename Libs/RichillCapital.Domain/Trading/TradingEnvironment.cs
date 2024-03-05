using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Trading;

public sealed class TradingEnvironment : Enumeration<TradingEnvironment>
{
    public static readonly TradingEnvironment Mock = new(nameof(Mock), 1);
    public static readonly TradingEnvironment Simulated = new(nameof(Simulated), 2);
    public static readonly TradingEnvironment Live = new(nameof(Live), 3);

    private TradingEnvironment(string name, int value)
        : base(name, value)
    {
    }
}