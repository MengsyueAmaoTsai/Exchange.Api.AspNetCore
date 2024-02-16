
namespace RichillCapital.SharedKernel.Monad;

public record class ErrorOr<TValue> : ErrorOr
{
    private ErrorOr(bool isError, Error error, TValue value)
        : base(isError, error) => Value = value;

    public TValue Value { get; private init; }

    public static ErrorOr<TValue> From(TValue value) =>
        new(false, Error.Default, value);

    public static implicit operator ErrorOr<TValue>(Error error) =>
        new(true, error, default);

    public static implicit operator ErrorOr<TValue>(TValue value) =>
        new(false, Error.Default, value);
}

public record class ErrorOr
{
    public static readonly ErrorOr NoError = new(false, Error.Default);

    internal protected ErrorOr(bool isError, Error error) =>
        (IsError, Error) = (isError, error);

    public bool IsError { get; private init; }

    public bool IsNoError => !IsError;

    public Error Error { get; private init; }

    public static ErrorOr<TValue> From<TValue>(TValue value) =>
        ErrorOr<TValue>.From(value);

    public static implicit operator ErrorOr(Error error) =>
        new(true, error);
}