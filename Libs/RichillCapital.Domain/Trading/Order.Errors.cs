using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Trading;

public static class OrderErrors
{
    public static Error InvalidQuantity(decimal quantity) =>
        Error.Invalid($"Quantity must be greater than zero. Provided quantity: '{quantity}'.");
}