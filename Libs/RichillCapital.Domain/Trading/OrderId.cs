using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Trading;

public sealed class OrderId : SingleValueObject<string>
{
    public const int MaxLength = 200;

    private OrderId(string value)
        : base(value)
    {
    }

    public static ErrorOr<OrderId> From(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return Error.Invalid("Order id cannot be empty.");
        }

        if (id.Length > MaxLength)
        {
            return Error.Invalid(
                $"Order id cannot be longer than {MaxLength} characters.");
        }

        return new OrderId(id);
    }

    public static OrderId NewOrderId()
    {
        return From(Guid.NewGuid().ToString()).Value;
    }
}