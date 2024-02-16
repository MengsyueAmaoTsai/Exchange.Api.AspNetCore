namespace RichillCapital.SharedKernel.Monad;

public record class Result<TValue> : Result
{
    private Result(bool isSuccess, Error error, TValue value)
        : base(isSuccess, error) => Value = value;

    public TValue Value { get; private init; }

    public static Result<TValue> From(TValue value) =>
        new(true, Error.Default, value);

    public static implicit operator Result<TValue>(Error error) =>
        new(false, error, default);

    public static implicit operator Result<TValue>(TValue value) =>
        new(true, Error.Default, value);
}

public record class Result
{
    public static readonly Result Success =
        new(true, Error.Default);

    internal protected Result(bool isSuccess, Error error) =>
        (IsSuccess, Error) = (isSuccess, error);

    public bool IsSuccess { get; private init; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; private init; }

    public static Result<TValue> From<TValue>(TValue value) =>
        Result<TValue>.From(value);

    public static implicit operator Result(Error error) =>
        new(false, error);
}