using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monad;

namespace RichillCapital.Domain.Trading;

public sealed class Position : Entity<PositionId>
{
    private Position(
        PositionId id,
        Side side,
        Symbol symbol,
        decimal quantity,
        decimal price,
        decimal commission,
        decimal tax,
        decimal swap,
        AccountId accountId)
        : base(id)
    {
        Side = side;
        Symbol = symbol;
        Quantity = quantity;
        Price = price;
        Commission = commission;
        Tax = tax;
        Swap = swap;
        AccountId = accountId;
    }

    public Side Side { get; private set; }

    public Symbol Symbol { get; private set; }

    public decimal Quantity { get; private set; }

    public decimal Price { get; private set; }

    public decimal Commission { get; private set; }

    public decimal Tax { get; private set; }

    public decimal Swap { get; private set; }

    public AccountId AccountId { get; private set; }

    public static ErrorOr<Position> Open(
        Side side,
        Symbol symbol,
        decimal quantity,
        decimal price,
        decimal commission,
        decimal tax,
        decimal swap,
        AccountId accountId)
    {
        if (quantity <= 0)
        {
            return Error.Invalid("Quantity must be greater than 0.");
        }

        if (commission < 0)
        {
            return Error.Invalid("Commission must be greater than or equal to 0.");
        }

        if (tax < 0)
        {
            return Error.Invalid("Tax must be greater than or equal to 0.");
        }

        if (swap < 0)
        {
            return Error.Invalid("Swap must be greater than or equal to 0.");
        }

        var position = new Position(
            PositionId.NewPositionId(),
            side,
            symbol,
            quantity,
            price,
            commission,
            tax,
            swap,
            accountId);

        return position;
    }
}