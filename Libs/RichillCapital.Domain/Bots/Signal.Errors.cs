using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Bots;

public static class SignalErrors
{
    public static Error InvalidVolume(decimal volume) =>
        Error.Invalid($"Volume must be greater than 0. Provided volume: '{volume}'.");

    public static Error InvalidPrice(decimal price) =>
        Error.Invalid($"Price must be greater than 0. Provided price: '{price}'.");
}