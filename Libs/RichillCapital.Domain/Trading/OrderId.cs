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
            return Error
                .Invalid("Order id cannot be empty.")
                .ToErrorOr<OrderId>();
        }

        if (id.Length > MaxLength)
        {
            return Error
                .Invalid($"Order id cannot be longer than {MaxLength} characters.")
                .ToErrorOr<OrderId>();
        }

        return new OrderId(id).ToErrorOr();
    }

    public static OrderId NewOrderId() => From(Guid.NewGuid().ToString()).Value;
}