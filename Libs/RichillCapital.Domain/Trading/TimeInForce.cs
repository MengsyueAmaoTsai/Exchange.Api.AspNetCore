using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Trading;

public sealed record class TimeInForce : Enumeration<TimeInForce>
{
    public static readonly TimeInForce IOC = new(nameof(IOC), 1);
    public static readonly TimeInForce FOK = new(nameof(FOK), 2);
    public static readonly TimeInForce Day = new(nameof(Day), 3);
    public static readonly TimeInForce GTD = new(nameof(GTD), 4);
    public static readonly TimeInForce GTC = new(nameof(GTC), 5);

    private TimeInForce(string name, int value)
        : base(name, value)
    {
    }
}