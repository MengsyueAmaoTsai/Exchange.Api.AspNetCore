using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monad;

namespace RichillCapital.Domain.Bots;

public sealed class BotDescription : SingleValueObject<string>
{
    public const int MaxLength = 1000;

    private BotDescription(string value)
        : base(value)
    {
    }

    public static ErrorOr<BotDescription> From(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            return Error.Invalid("Bot description cannot be empty.");
        }

        if (description.Length > MaxLength)
        {
            return Error.Invalid($"Bot description cannot be longer than {MaxLength} characters.");
        }

        return new BotDescription(description);
    }
}