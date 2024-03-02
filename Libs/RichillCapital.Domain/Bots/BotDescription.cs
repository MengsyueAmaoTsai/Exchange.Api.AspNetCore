using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Bots;

public sealed class BotDescription : SingleValueObject<string>
{
    public const int MaxLength = 1000;

    private BotDescription(string value)
        : base(value)
    {
    }

    public static Result<BotDescription> From(string description) => throw new NotImplementedException();
    // description
    //     .ToResult()
    //     .Ensure(NotEmpty, Error.Invalid("Bot description cannot be empty."))
    //     .Ensure(NotLongerThanMaxLength, Error.Invalid($"Bot description cannot be longer than {MaxLength} characters."))
    //     .Then(description => new BotDescription(description));

    private static bool NotEmpty(string description) => !string.IsNullOrWhiteSpace(description);

    private static bool NotLongerThanMaxLength(string description) => description.Length <= MaxLength;
}