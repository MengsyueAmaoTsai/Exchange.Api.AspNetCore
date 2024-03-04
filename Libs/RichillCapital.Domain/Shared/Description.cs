using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Shared;

public class Description : SingleValueObject<string>
{
    public const int MaxLength = 1000;

    protected Description(string value)
        : base(value)
    {
    }

    public static Result<Description> From(string description) =>
        description.ToResult()
            .Ensure(NotLongerThanMaxLength, Error.Invalid($"Bot description cannot be longer than {MaxLength} characters."))
            .Then(description => new Description(description));

    protected static bool NotLongerThanMaxLength(string description) => description.Length <= MaxLength;
}
