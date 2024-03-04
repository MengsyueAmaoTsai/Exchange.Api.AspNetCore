using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Shared;

public sealed class NonEmptyDescription : Description
{
    private NonEmptyDescription(string value)
        : base(value)
    {
    }

    public new static Result<NonEmptyDescription> From(string description) =>
        description.ToResult()
            .Ensure(NotEmpty, Error.Invalid("Bot description cannot be empty."))
            .Ensure(NotLongerThanMaxLength, Error.Invalid($"Bot description cannot be longer than {MaxLength} characters."))
            .Then(description => new NonEmptyDescription(description));

    private static bool NotEmpty(string description) => !string.IsNullOrWhiteSpace(description);
}
