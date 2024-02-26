using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Bots;

public sealed class BotName : SingleValueObject<string>
{
    public const int MaxLength = 100;

    private BotName(string value)
        : base(value)
    {
    }

    public static ErrorOr<BotName> From(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            Error
                .Invalid("Bot name cannot be empty.")
                .ToErrorOr<BotName>();
        }

        if (name.Length > MaxLength)
        {
            Error
                .Invalid("Bot name cannot be longer than 100 characters.")
                .ToErrorOr<BotName>();
        }

        return new BotName(name)
            .ToErrorOr();
    }
}