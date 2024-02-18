using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Trading;

public sealed class OrderType : Enumeration<OrderType>
{
    public static readonly OrderType Market = new(nameof(Market), 1);
    public static readonly OrderType Limit = new(nameof(Limit), 2);
    public static readonly OrderType StopMarket = new(nameof(StopMarket), 3);
    public static readonly OrderType StopLimit = new(nameof(StopLimit), 4);

    private OrderType(string name, int value)
        : base(name, value)
    {
    }
}