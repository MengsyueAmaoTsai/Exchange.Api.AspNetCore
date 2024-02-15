
namespace RichillCapital.SharedKernel.Monad;

public readonly record struct ErrorOr<TValue>
{
    private ErrorOr(bool isError, Error error, TValue value)
    {
        IsError = isError;
        Error = error;
        Value = value;
    }

    public bool IsError { get; private init; }

    public Error Error { get; private init; }

    public TValue Value { get; private init; }


    public static ErrorOr<TValue> From(TValue value) => new(false, Error.Default, value);

    public static implicit operator ErrorOr<TValue>(Error error) => new(true, error, default);

    public static implicit operator ErrorOr<TValue>(TValue value) => new(false, Error.Default, value);
}

public readonly record struct ErrorOr
{
    public static ErrorOr<TValue> From<TValue>(TValue value) => ErrorOr<TValue>.From(value);
}