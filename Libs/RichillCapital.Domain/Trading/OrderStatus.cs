using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Trading;

public sealed record class OrderStatus : Enumeration<OrderStatus>
{
    public static readonly OrderStatus New = new(nameof(New), 1);
    public static readonly OrderStatus Rejected = new(nameof(Rejected), 2);
    public static readonly OrderStatus Pending = new(nameof(Pending), 3);
    public static readonly OrderStatus Cancelled = new(nameof(Cancelled), 4);
    public static readonly OrderStatus Expired = new(nameof(Expired), 5);
    public static readonly OrderStatus PartiallyFilled = new(nameof(PartiallyFilled), 6);
    public static readonly OrderStatus Executed = new(nameof(Executed), 7);

    private OrderStatus(string name, int value)
        : base(name, value)
    {
    }
}