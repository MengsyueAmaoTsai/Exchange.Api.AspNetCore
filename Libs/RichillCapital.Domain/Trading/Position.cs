using RichillCapital.Domain.Shared;
using RichillCapital.Domain.Trading.Events;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

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
        AccountId accountId)
    {
        if (quantity <= 0)
        {
            return PositionErrors.InvalidQuantity(quantity).ToErrorOr<Position>();
        }

        if (commission < 0)
        {
            return PositionErrors.InvalidCommission(commission).ToErrorOr<Position>();
        }

        if (tax < 0)
        {
            return PositionErrors.InvalidTax(tax).ToErrorOr<Position>();
        }

        return new Position(
                PositionId.NewPositionId(),
                side,
                symbol,
                quantity,
                price,
                commission,
                tax,
                decimal.Zero,
                accountId).ToErrorOr();
    }

    public void Close()
    {
        RegisterDomainEvent(new AccountPositionClosedDomainEvent(Id, AccountId));
    }
}