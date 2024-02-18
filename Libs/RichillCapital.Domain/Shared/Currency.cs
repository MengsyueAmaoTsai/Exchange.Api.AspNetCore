using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Shared;

public sealed class Currency : Enumeration<Currency>
{
    public static readonly Currency TWD = new(nameof(TWD), 1);
    public static readonly Currency USD = new(nameof(USD), 2);
    public static readonly Currency USDT = new(nameof(USDT), 3);

    private Currency(string name, int value)
        : base(name, value)
    {
    }
}