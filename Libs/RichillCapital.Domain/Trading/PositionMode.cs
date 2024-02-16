using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Trading;

public sealed record class PositionMode : Enumeration<PositionMode>
{
    public static readonly PositionMode Hedging = new(nameof(Hedging), 1);
    public static readonly PositionMode Netting = new(nameof(Netting), 2);

    private PositionMode(string name, int value)
        : base(name, value)
    {
    }
}