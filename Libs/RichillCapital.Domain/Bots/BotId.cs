using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Bots;

public sealed class BotId : SingleValueObject<string>
{
    public const int MaxLength = 36;

    private BotId(string value)
        : base(value)
    {
    }

    public static BotId From(string id)
    {
        return new BotId(id);
    }
}