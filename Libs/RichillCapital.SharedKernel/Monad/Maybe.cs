namespace RichillCapital.SharedKernel.Monad;

public readonly record struct Maybe<TValue>
{
    public static Maybe<TValue> Null = new(false, default);

    private Maybe(bool hasValue, TValue value)
    {
        HasValue = hasValue;
        Value = value;
    }

    public bool HasValue { get; private init; }

    public bool HasNoValue => !HasValue;

    public TValue Value { get; private init; }

    public static Maybe<TValue> From(TValue value) => new(true, value);

    public static implicit operator Maybe<TValue>(TValue? value) =>
        value is null ? Maybe<TValue>.Null : From(value);
}