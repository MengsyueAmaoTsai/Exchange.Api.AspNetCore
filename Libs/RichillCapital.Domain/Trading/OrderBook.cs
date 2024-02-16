using System.Collections.ObjectModel;

using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monad;

namespace RichillCapital.Domain.Trading;

public sealed class OrderBook : ValueObject
{
    private OrderBook(IEnumerable<OrderBookEntry> bids, IEnumerable<OrderBookEntry> asks) =>
        (Bids, Asks) = (
            new ReadOnlyCollection<OrderBookEntry>(bids.ToList()),
            new ReadOnlyCollection<OrderBookEntry>(asks.ToList()));

    public IReadOnlyCollection<OrderBookEntry> Bids { get; private set; }

    public IReadOnlyCollection<OrderBookEntry> Asks { get; private set; }

    public static Result<OrderBook> Create(
        IEnumerable<OrderBookEntry> bids,
        IEnumerable<OrderBookEntry> asks) =>
        !bids.Any() && !asks.Any() ?
            Error.Invalid("Order book must contain at least one bid or ask.") :
            new OrderBook(bids, asks);

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Bids;
        yield return Asks;
    }
}

public sealed class OrderBookEntry : ValueObject
{
    private OrderBookEntry(decimal price, decimal quantity) =>
        (Price, Quantity) = (price, quantity);

    public decimal Price { get; private set; }

    public decimal Quantity { get; private set; }

    public static Result<OrderBookEntry> Create(decimal price, decimal quantity) =>
        quantity <= decimal.Zero ?
            Error.Invalid("Quantity must be greater than 0.") :
            new OrderBookEntry(price, quantity);

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Price;
        yield return Quantity;
    }
}