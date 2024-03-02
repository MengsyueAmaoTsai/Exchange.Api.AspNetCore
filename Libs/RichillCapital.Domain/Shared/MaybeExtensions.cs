namespace RichillCapital.SharedKernel.Monads;

public static partial class MaybeExtensions
{
    public static Maybe<TValue> Then<TValue>(this Maybe<TValue> result, Action action)
    {
        if (result.IsNull)
        {
            return Maybe<TValue>.Null;
        }

        action();

        return result;
    }
    public static Maybe<TValue> Then<TValue>(this Maybe<TValue> result, Action<TValue> actionWithValue)
    {
        if (result.IsNull)
        {
            return Maybe<TValue>.Null;
        }

        actionWithValue(result.Value);

        return result;
    }
    public static Maybe<TResult> Then<TValue, TResult>(this Maybe<TValue> result, Func<TResult> factory)
    {
        if (result.IsNull)
        {
            return Maybe<TResult>.Null;
        }

        return factory().ToMaybe();
    }

    public static Maybe<TResult> Then<TValue, TResult>(
        this Maybe<TValue> result,
        Func<TValue, TResult> factoryWithValue)
    {
        if (result.IsNull)
        {
            return Maybe<TResult>.Null;
        }

        return factoryWithValue(result.Value).ToMaybe();
    }

    public static Maybe<TResult> Then<TValue, TResult>(
        this Maybe<TValue> maybe,
        Func<TValue, Maybe<TResult>> maybeFactoryWithValue)
    {
        if (maybe.IsNull)
        {
            return Maybe<TResult>.Null;
        }

        return maybeFactoryWithValue(maybe.Value);
    }

    // ----- Async
    public static async Task<Maybe<TResult>> Then<TValue, TResult>(
        this Maybe<TValue> result,
        Func<TValue, Task<Maybe<TResult>>> maybeFactoryWithValueTask)
    {
        if (result.IsNull)
        {
            return Maybe<TResult>.Null;
        }

        var resultResult = await maybeFactoryWithValueTask(result.Value);

        return resultResult;
    }

    public static async Task<Maybe<TResult>> Then<TValue, TResult>(
        this Task<Maybe<TValue>> maybeTask,
        Func<TValue, TResult> factoryWithValue)
    {
        var result = await maybeTask;

        if (result.IsNull)
        {
            return Maybe<TResult>.Null;
        }

        return factoryWithValue(result.Value).ToMaybe();
    }
}