using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monad;

namespace RichillCapital.Domain.Trading;

public sealed class PositionId : SingleValueObject<string>
{
    public const int MaxLength = 200;

    private PositionId(string value)
        : base(value)
    {
    }

    public static ErrorOr<PositionId> From(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return Error.Invalid("Position id cannot be empty.");
        }

        if (id.Length > MaxLength)
        {
            return Error.Invalid(
                $"Position id cannot be longer than {MaxLength} characters.");
        }

        return new PositionId(id);
    }

    public static PositionId NewPositionId()
    {
        return From(Guid.NewGuid().ToString()).Value;
    }
}