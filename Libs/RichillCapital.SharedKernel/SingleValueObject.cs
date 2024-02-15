namespace RichillCapital.SharedKernel;

public abstract class SingleValueObject<TValue> : ValueObject
    where TValue : notnull
{
    protected SingleValueObject(TValue value) => Value = value;

    public TValue Value { get; private init; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}