using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Trading;

public sealed partial class Trade : ValueObject
{
    private Trade(
        Side side,
        Symbol symbol,
        decimal quantity,
        DateTimeOffset entryTime,
        decimal entryPrice,
        DateTimeOffset exitTime,
        decimal exitPrice,
        decimal commission,
        decimal tax,
        decimal swap,
        AccountId accountId)
    {
        Side = side;
        Symbol = symbol;
        Quantity = quantity;
        EntryTime = entryTime;
        EntryPrice = entryPrice;
        ExitTime = exitTime;
        ExitPrice = exitPrice;
        Commission = commission;
        Tax = tax;
        Swap = swap;
        AccountId = accountId;
    }

    public Side Side { get; private set; }

    public Symbol Symbol { get; private set; }

    public decimal Quantity { get; private set; }

    public DateTimeOffset EntryTime { get; private set; }

    public decimal EntryPrice { get; private set; }

    public DateTimeOffset ExitTime { get; private set; }

    public decimal ExitPrice { get; private set; }

    public decimal Commission { get; private set; }

    public decimal Tax { get; private set; }

    public decimal Swap { get; private set; }

    public AccountId AccountId { get; private set; }

    public static ErrorOr<Trade> Create(
        Side side,
        Symbol symbol,
        decimal quantity,
        DateTimeOffset entryTime,
        decimal entryPrice,
        DateTimeOffset exitTime,
        decimal exitPrice,
        decimal commission,
        decimal tax,
        decimal swap,
        AccountId accountId)
    {
        if (quantity <= 0)
        {
            return TradeErrors
                .InvalidQuantity(quantity)
                .ToErrorOr<Trade>();
        }

        if (entryPrice <= 0)
        {
            return TradeErrors
                .InvalidEntryPrice(entryPrice)
                .ToErrorOr<Trade>();
        }

        if (exitPrice <= 0)
        {
            return TradeErrors
                .InvalidExitPrice(exitPrice)
                .ToErrorOr<Trade>();
        }

        if (commission < 0)
        {
            return TradeErrors
                .InvalidCommission(commission)
                .ToErrorOr<Trade>();
        }

        if (tax < 0)
        {
            return TradeErrors
                .InvalidTax(tax)
                .ToErrorOr<Trade>();
        }

        return ErrorOr<Trade>
            .Is(new Trade(
                side,
                symbol,
                quantity,
                entryTime,
                entryPrice,
                exitTime,
                exitPrice,
                commission,
                tax,
                swap,
                accountId));
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Side;
        yield return Symbol;
        yield return Quantity;
        yield return EntryTime;
        yield return EntryPrice;
        yield return ExitTime;
        yield return ExitPrice;
        yield return Commission;
        yield return Tax;
        yield return Swap;
        yield return AccountId;
    }
}