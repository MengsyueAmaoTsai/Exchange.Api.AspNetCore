namespace RichillCapital.SharedKernel.Monads;

public static partial class MaybeExtensions
{
    public static async Task<Maybe<TResult>> Then<TValue, TResult>(
        this Maybe<TValue> maybe,
        Func<TValue, Task<Result<TResult>>> resultFactoryWithValueTask)
    {
        if (maybe.IsNull)
        {
            return Maybe<TResult>.Null;
        }

        var result = await resultFactoryWithValueTask(maybe.Value);

        return result.ToMaybe();
    }

    public static async Task<Maybe<TResult>> Then<TValue, TResult>(
        this Maybe<TValue> maybe,
        Func<TValue, Task<ErrorOr<TResult>>> errorOrFactoryWithValueTask)
    {
        if (maybe.IsNull)
        {
            return Maybe<TResult>.Null;
        }

        var errorOr = await errorOrFactoryWithValueTask(maybe.Value);

        return errorOr.ToMaybe();
    }
}