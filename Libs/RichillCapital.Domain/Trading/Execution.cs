using RichillCapital.Domain.Shared;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Trading;

public sealed class Execution : ValueObject
{
    private Execution(
        DateTimeOffset time,
        TradeType tradeType,
        Symbol symbol,
        decimal quantity,
        decimal price,
        decimal commission,
        decimal tax,
        AccountId accountId,
        OrderId orderId)
    {
        Time = time;
        TradeType = tradeType;
        Symbol = symbol;
        Quantity = quantity;
        Price = price;
        Commission = commission;
        Tax = tax;
        AccountId = accountId;
        OrderId = orderId;
    }

    public DateTimeOffset Time { get; private set; }

    public TradeType TradeType { get; private set; }

    public Symbol Symbol { get; private set; }

    public decimal Quantity { get; private set; }

    public decimal Price { get; private set; }

    public decimal Commission { get; private set; }

    public decimal Tax { get; private set; }

    public AccountId AccountId { get; private set; }

    public OrderId OrderId { get; private set; }

    public static ErrorOr<Execution> Create(
        TradeType tradeType,
        Symbol symbol,
        decimal quantity,
        decimal price,
        decimal commission,
        decimal tax,
        AccountId accountId,
        OrderId orderId)
    {
        if (quantity <= 0)
        {
            return ExecutionErrors
                .InvalidQuantity(quantity)
                .ToErrorOr<Execution>();
        }

        if (price <= 0)
        {
            return ExecutionErrors
                .InvalidPrice(price)
                .ToErrorOr<Execution>();
        }

        if (commission < 0)
        {
            return ExecutionErrors
                .InvalidCommission(commission)
                .ToErrorOr<Execution>();
        }

        if (tax < 0)
        {
            return ExecutionErrors
                .InvalidTax(tax)
                .ToErrorOr<Execution>();
        }

        return new Execution(
            DateTimeOffset.UtcNow,
            tradeType,
            symbol,
            quantity,
            price,
            commission,
            tax,
            accountId,
            orderId).ToErrorOr();
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return TradeType;
        yield return Symbol;
        yield return Quantity;
        yield return Price;
        yield return AccountId;
        yield return OrderId;
    }
}