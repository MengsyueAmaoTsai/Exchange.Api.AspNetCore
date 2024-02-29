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
        Result<string>
            .Ensure(id, BotIdRules.Values)
            .Then(value => new BotId(value));
}

internal static class BotIdRules
{
    internal static readonly (Func<string, bool> predicate, Error error)[] Values = [
        (id => !string.IsNullOrWhiteSpace(id), Empty),
        (id => id.Length <= BotId.MaxLength, TooLong)
    ];

    private static readonly Error Empty = Error.Invalid("Bot id cannot be empty.");
    private static readonly Error TooLong = Error
        .Invalid($"Bot id cannot be longer than {BotId.MaxLength} characters.");
}