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

    public static Result<BotId> From(string id) =>
        id.ToResult()
            .Ensure(NotEmpty, Error.Invalid("Bot id cannot be empty."))
            .Ensure(NotLongerThanMaxLength, Error.Invalid($"Bot id cannot be longer than {MaxLength} characters."))
            .Then(id => new BotId(id));

    private static bool NotEmpty(string id) => !string.IsNullOrWhiteSpace(id);

    private static bool NotLongerThanMaxLength(string id) => id.Length <= MaxLength;
}