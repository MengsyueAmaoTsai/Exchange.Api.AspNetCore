using RichillCapital.Domain.Trading.Events;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Trading;

public sealed class Order : Entity<OrderId>
{
    private readonly List<Execution> _executions = [];

    private Order(
        DateTimeOffset time,
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
        Time = time;
        TradeType = tradeType;
        Quantity = quantity;
        RemainingQuantity = remainingQuantity;
        Symbol = symbol;
        Type = type;
        TimeInForce = timeInForce;
        Status = status;
        AccountId = accountId;
    }

    public DateTimeOffset Time { get; private set; }

    public TradeType TradeType { get; private set; }

    public decimal Quantity { get; private set; }

    public decimal RemainingQuantity { get; private set; }

    public Symbol Symbol { get; private set; }

    public OrderType Type { get; private set; }

    public TimeInForce TimeInForce { get; private set; }

    public OrderStatus Status { get; private set; }

    public AccountId AccountId { get; private set; }

    public IReadOnlyList<Execution> Executions => _executions.AsReadOnly();

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
            return OrderErrors.InvalidQuantity(quantity);
        }

        if (type == OrderType.Market && timeInForce == TimeInForce.Day)
        {
            return Error.Invalid("Market orders cannot have time in force set to day.");
        }

        return ErrorOr<Order>.Is(new Order(
            DateTimeOffset.UtcNow,
            OrderId.NewOrderId(),
            tradeType,
            quantity,
            quantity,
            symbol,
            type,
            timeInForce,
            OrderStatus.New,
            accountId));
    }

    public Result Reject()
    {
        if (Status != OrderStatus.New)
        {
            return Error.Conflict("Only new orders can be rejected.");
        }

        Status = OrderStatus.Rejected;

        RegisterDomainEvent(new AccountOrderRejectedDomainEvent(Id));

        return Result.Success();
    }

    public Result Accept()
    {
        if (Status != OrderStatus.New)
        {
            return Error.Conflict("Only new orders can be accepted.");
        }

        Status = OrderStatus.Pending;

        RegisterDomainEvent(new AccountOrderAcceptedDomainEvent(Id));

        return Result.Success();
    }

    public Result Cancel()
    {
        if (Status != OrderStatus.Pending)
        {
            return Error.Conflict("Only pending orders can be cancelled.");
        }

        Status = OrderStatus.Cancelled;

        RegisterDomainEvent(new AccountOrderCancelledDomainEvent(Id));

        return Result.Success();
    }

    public ErrorOr<Execution> Execute(
        decimal executionQuantity,
        decimal executionPrice,
        decimal commission,
        decimal tax)
    {
        if (Status != OrderStatus.Pending &&
            Status != OrderStatus.PartiallyFilled)
        {
            return Error.Invalid("Only pending or partially filled orders can be executed.");
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

        if (execution.IsError)
        {
            return execution.Errors;
        }

        Quantity -= executionQuantity;

        Status = Quantity == decimal.Zero ?
            OrderStatus.Executed :
            OrderStatus.PartiallyFilled;

        _executions.Add(execution.Value);

        RegisterDomainEvent(new AccountOrderExecutedDomainEvent(execution.Value));

        return execution.Value;
    }
}