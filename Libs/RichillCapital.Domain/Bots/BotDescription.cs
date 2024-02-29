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

    public static Result<BotDescription> From(string description) =>
        Result<string>
            .Ensure(description, BotDescriptionRules.Values)
            .Then(value => new BotDescription(value));
}

internal static class BotDescriptionRules
{
    public static readonly (Func<string, bool> predicate, Error error)[] Values = [
        IsNotEmpty,
        IsNotLongerThan
    ];

    private static readonly Error Empty = Error.Invalid("Bot description cannot be empty.");
    private static readonly Error TooLong = Error
        .Invalid($"Bot description cannot be longer than {BotDescription.MaxLength} characters.");

    private static readonly (Func<string, bool> predicate, Error error) IsNotEmpty = (
        description => !string.IsNullOrWhiteSpace(description),
        Empty
    );

    private static readonly (Func<string, bool> predicate, Error error) IsNotLongerThan = (
        description => description.Length <= BotDescription.MaxLength,
        TooLong
    );
}