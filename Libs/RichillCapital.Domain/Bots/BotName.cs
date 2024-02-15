using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Bots;

public sealed class BotName : SingleValueObject<string>
{
    public const int MaxLength = 100;

    private BotName(string value)
        : base(value)
    {
    }

    public static BotName From(string name)
    {
        return new BotName(name);
    }
}