using System.Diagnostics;

namespace RichillCapital.SharedKernel.Monads;

public static partial class MaybeExtensions
{
    public static Maybe<TResult> Then<TValue, TResult>(
        this Maybe<TValue> maybe,
        Func<TValue, TResult> factory)
    {
        if (maybe.IsNull)
        {
            return Maybe<TResult>.Null;
        }

        return factory(maybe.Value).ToMaybe();
    }
}