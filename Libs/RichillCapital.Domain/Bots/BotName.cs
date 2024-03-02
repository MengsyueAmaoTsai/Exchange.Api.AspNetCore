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

    public static Result<BotName> From(string name) => throw new NotFiniteNumberException();
    // name.ToResult()
    //     .Ensure(NotEmpty, Error.Invalid("Bot name cannot be empty."))
    //     .Ensure(NotLongerThanMaxLength, Error.Invalid($"Bot name cannot be longer than {MaxLength} characters."))
    //     .Then(name => new BotName(name));

    private static bool NotEmpty(string name) => !string.IsNullOrWhiteSpace(name);

    private static bool NotLongerThanMaxLength(string name) => name.Length <= MaxLength;
}
