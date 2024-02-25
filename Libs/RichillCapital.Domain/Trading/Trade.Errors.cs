using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Trading;

public static class TradeErrors
{
    public static Error InvalidQuantity(decimal quantity) =>
        Error.Invalid($"Quantity must be greater than zero. Provided quantity: '{quantity}'.");

    public static Error InvalidEntryPrice(decimal price) =>
        Error.Invalid($"Entry price must be greater than zero. Provided price: '{price}'.");

    public static Error InvalidExitPrice(decimal price) =>
        Error.Invalid($"Exit price must be greater than zero. Provided price: '{price}'.");

    public static Error InvalidCommission(decimal commission) =>
        Error.Invalid($"Commission must be greater than or equal to zero. Provided commission: '{commission}'.");

    public static Error InvalidTax(decimal tax) =>
        Error.Invalid($"Tax must be greater than or equal to zero. Provided tax: '{tax}'.");
}