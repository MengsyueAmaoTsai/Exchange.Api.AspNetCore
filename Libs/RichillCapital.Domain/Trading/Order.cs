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
        decimal remainingQuantity,
        Symbol symbol,
        OrderType type,
        TimeInForce timeInForce,
        OrderStatus status,
        AccountId accountId)
        : base(id)
    {
        TradeType = tradeType;
        Quantity = quantity;
        RemainingQuantity = remainingQuantity;
        Symbol = symbol;
        Type = type;
        TimeInForce = timeInForce;
        Status = status;
        AccountId = accountId;
    }

    public TradeType TradeType { get; private set; }

    public decimal Quantity { get; private set; }

    public decimal RemainingQuantity { get; private set; }

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

        if (type == OrderType.Market && timeInForce == TimeInForce.Day)
        {
            return DomainErrors.Orders.InvalidTimeInForce(type, timeInForce);
        }

        var order = new Order(
            OrderId.NewOrderId(),
            tradeType,
            quantity,
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

    public Result Cancel()
    {
        if (Status != OrderStatus.Pending)
        {
            return Error.Conflict("Only pending orders can be cancelled.");
        }

        Status = OrderStatus.Cancelled;

        RegisterDomainEvent(new AccountOrderCancelledDomainEvent(Id));

        return Result.Success;
    }

    public ErrorOr<Execution> Execute(
        decimal executionQuantity,
        decimal executionPrice,
        decimal commission,
        decimal tax)
    {
        if (Status != OrderStatus.Pending)
        {
            return Error.Conflict("Only pending orders can be executed.");
        }

        if (executionQuantity > Quantity)
        {
            return Error.Invalid("Execution quantity cannot be greater than order quantity.");
        }

        var execution = Execution.Create(
            TradeType,
            Symbol,
            executionQuantity,
            executionPrice,
            commission,
            tax,
            AccountId,
            Id);

        Quantity -= executionQuantity;

        Status = Quantity == decimal.Zero ?
            OrderStatus.Executed :
            OrderStatus.PartiallyFilled;

        if (execution.IsFailure)
        {
            return execution.Error;
        }

        return execution.Value;
    }
}