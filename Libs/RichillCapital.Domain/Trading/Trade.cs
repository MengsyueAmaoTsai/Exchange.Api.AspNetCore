using RichillCapital.SharedKernel;

namespace RichillCapital.Domain.Trading;

public sealed class Trade : ValueObject
{
    private Trade()
    {
    }

    public static Trade Create()
    {
        return new Trade();
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        throw new NotImplementedException();
    }
}