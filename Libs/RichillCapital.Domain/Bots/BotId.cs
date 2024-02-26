using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Bots;

public sealed class BotId : SingleValueObject<string>
{
    public const int MaxLength = 36;

    private BotId(string value)
        : base(value)
    {
    }

    public static ErrorOr<BotId> From(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return Error
                .Invalid("Bot id cannot be empty.")
                .ToErrorOr<BotId>();
        }

        if (id.Length > MaxLength)
        {
            return Error
                .Invalid("Bot id cannot be longer than 36 characters.")
                .ToErrorOr<BotId>();
        }

        return new BotId(id).ToErrorOr();
    }
}