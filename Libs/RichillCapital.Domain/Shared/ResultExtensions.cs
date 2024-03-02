namespace RichillCapital.SharedKernel.Monads;

public static partial class ResultExtensions
{
    public static Result<TValue> Then<TValue>(this Result<TValue> result, Action action)
    {
        if (result.IsFailure)
        {
            return result.Error.ToResult<TValue>();
        }

        action();

        return result;
    }

    public static Result<TValue> Then<TValue>(
        this Result<TValue> result,
        Action<TValue> actionWithValue)
    {
        if (result.IsFailure)
        {
            return result.Error.ToResult<TValue>();
        }

        actionWithValue(result.Value);

        return result;
    }
    public static Result<TResult> Then<TValue, TResult>(
        this Result<TValue> result,
        Func<TResult> factory)
    {
        if (result.IsFailure)
        {
            return result.Error.ToResult<TResult>();
        }

        return factory().ToResult();
    }

    public static Result<TResult> Then<TValue, TResult>(
        this Result<TValue> result,
        Func<TValue, TResult> factoryWithValue)
    {
        if (result.IsFailure)
        {
            return result.Error.ToResult<TResult>();
        }

        return factoryWithValue(result.Value).ToResult();
    }

    public static Result<TResult> Then<TValue, TResult>(
        this Result<TValue> result,
        Func<TValue, Result<TResult>> resultFactoryWithValue)
    {
        if (result.IsFailure)
        {
            return result.Error.ToResult<TResult>();
        }

        return resultFactoryWithValue(result.Value);
    }

    // ----- Async
    public static async Task<Result<TResult>> Then<TValue, TResult>(
        this Result<TValue> result,
        Func<TValue, Task<Result<TResult>>> resultFactoryWithValueTask)
    {
        if (result.IsFailure)
        {
            return result.Error.ToResult<TResult>();
        }

        var resultResult = await resultFactoryWithValueTask(result.Value);

        return resultResult;
    }

    public static async Task<Result<TResult>> Then<TValue, TResult>(
        this Task<Result<TValue>> resultTask,
        Func<TValue, TResult> factoryWithValue)
    {
        var result = await resultTask;

        if (result.IsFailure)
        {
            return result.Error.ToResult<TResult>();
        }

        return factoryWithValue(result.Value).ToResult();
    }

    public static async Task<Result<TResult>> Then<TValue, TResult>(
        this Result<TValue> result,
        Func<TValue, Task<Maybe<TResult>>> maybeFactoryWithValueTask,
        Func<TValue, Error> errorFactoryWithValue)
    {
        if (result.IsFailure)
        {
            return result.Error.ToResult<TResult>();
        }

        var maybe = await maybeFactoryWithValueTask(result.Value);

        if (maybe.IsNull)
        {
            return errorFactoryWithValue(result.Value).ToResult<TResult>();
        }

        return maybe.Value.ToResult();
    }
}