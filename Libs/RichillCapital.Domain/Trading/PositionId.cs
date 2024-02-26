using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

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
            return Error
                .Invalid("Position id cannot be empty.")
                .ToErrorOr<PositionId>();
        }

        if (id.Length > MaxLength)
        {
            return Error
                .Invalid($"Position id cannot be longer than {MaxLength} characters.")
                .ToErrorOr<PositionId>();
        }

        return new PositionId(id).ToErrorOr();
    }

    public static PositionId NewPositionId()
    {
        return From(Guid.NewGuid().ToString()).Value;
    }
}