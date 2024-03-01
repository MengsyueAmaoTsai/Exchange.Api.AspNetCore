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

    public static Result<BotName> From(string name) => throw new NotImplementedException();
    // Result<string>
    //     .Ensure(name, BotNameRules.Values)
    //     .Then(value => new BotName(value));
}

internal static class BotNameRules
{
    public static readonly (Func<string, bool> predicate, Error error)[] Values = [
        (name => !string.IsNullOrWhiteSpace(name), Empty),
        (name => name.Length <= BotName.MaxLength, TooLong)
    ];

    private static readonly Error Empty = Error.Invalid("Bot name cannot be empty.");
    private static readonly Error TooLong = Error
        .Invalid($"Bot name cannot be longer than {BotName.MaxLength} characters.");
}