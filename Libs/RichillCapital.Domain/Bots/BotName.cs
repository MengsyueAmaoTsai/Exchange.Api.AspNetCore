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

    public static Result<BotName> From(string name) =>
        Result<string>.Success(name)
            .Ensure(BotNameRules.IsNotEmpty)
            .Ensure(BotNameRules.IsNotLongerThan)
            .Then(value => new BotName(value));
}

internal static class BotNameRules
{
    public static readonly (Func<string, bool> predicate, Error error) IsNotEmpty = (
        name => !string.IsNullOrWhiteSpace(name),
        Error.Invalid("Bot name cannot be empty."));

    internal static readonly (Func<string, bool> predicate, Error error) IsNotLongerThan = (
        name => name.Length <= BotName.MaxLength,
        Error.Invalid($"Bot name cannot be longer than {BotName.MaxLength} characters."));
}