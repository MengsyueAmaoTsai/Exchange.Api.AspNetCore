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
        Result<string>.Success(id)
            .Ensure(BotIdRules.IsNotEmpty)
            .Ensure(BotIdRules.IsNotLongerThan)
            .Then(value => new BotId(id));
}

internal static class BotIdRules
{
    public static readonly (Func<string, bool> predicate, Error error) IsNotEmpty = (
        id => !string.IsNullOrWhiteSpace(id),
        Error.Invalid("Bot id cannot be empty."));

    internal static readonly (Func<string, bool> predicate, Error error) IsNotLongerThan = (
        id => id.Length <= BotId.MaxLength,
        Error.Invalid($"Bot id cannot be longer than {BotId.MaxLength} characters."));
}