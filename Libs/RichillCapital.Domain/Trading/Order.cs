using RichillCapital.Domain.Trading.Events;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monad;

namespace RichillCapital.Domain.Trading;

public sealed class Order : Entity<OrderId>
{
    private Order(
        OrderId id,
        TradeType tradeType,
        decimal quantity,
        Symbol symbol,
        OrderType type,
        TimeInForce timeInForce,
        OrderStatus status,
        AccountId accountId)
        : base(id)
    {
        TradeType = tradeType;
        Quantity = quantity;
        Symbol = symbol;
        Type = type;
        TimeInForce = timeInForce;
        Status = status;
        AccountId = accountId;
    }

    public TradeType TradeType { get; private set; }

    public decimal Quantity { get; private set; }

    public Symbol Symbol { get; private set; }

    public OrderType Type { get; private set; }

    public TimeInForce TimeInForce { get; private set; }

    public OrderStatus Status { get; private set; }

    public AccountId AccountId { get; private set; }

    public static ErrorOr<Order> Create(
        TradeType tradeType,
        decimal quantity,
        Symbol symbol,
        OrderType type,
        TimeInForce timeInForce,
        AccountId accountId)
    {
        if (quantity <= 0)
        {
            return Error.Invalid("Quantity must be greater than 0.");
        }

        var order = new Order(
            OrderId.NewOrderId(),
            tradeType,
            quantity,
            symbol,
            type,
            timeInForce,
            OrderStatus.New,
            accountId);

        return order;
    }

    public Result Reject()
    {
        if (Status != OrderStatus.New)
        {
            return Error.Conflict("Only new orders can be rejected.");
        }

        Status = OrderStatus.Rejected;

        RegisterDomainEvent(new AccountOrderRejectedDomainEvent(Id));

        return Result.Success;
    }

    public Result Accept()
    {
        if (Status != OrderStatus.New)
        {
            return Error.Conflict("Only new orders can be accepted.");
        }

        Status = OrderStatus.Pending;

        RegisterDomainEvent(new AccountOrderAcceptedDomainEvent(Id));

        return Result.Success;
    }
}