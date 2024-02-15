using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Bots;

public sealed class Description : SingleValueObject<string>
{
    private Description(string value)
        : base(value)
    {
    }

    public static Description From(string description)
    {
        return new Description(description);
    }
}