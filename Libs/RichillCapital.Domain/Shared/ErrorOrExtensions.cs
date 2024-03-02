namespace RichillCapital.SharedKernel.Monads;

public static partial class ErrorOrExtensions
{
    public static ErrorOr<TValue> Then<TValue>(this ErrorOr<TValue> result, Action action)
    {
        if (result.HasError)
        {
            return result.Errors
                .ToErrorOr<TValue>();
        }

        action();

        return result;
    }
    public static ErrorOr<TValue> Then<TValue>(this ErrorOr<TValue> result, Action<TValue> actionWithValue)
    {
        if (result.HasError)
        {
            return result.Errors
                .ToErrorOr<TValue>();
        }

        actionWithValue(result.Value);

        return result;
    }

    public static ErrorOr<TResult> Then<TValue, TResult>(this ErrorOr<TValue> result, Func<TResult> factory)
    {
        if (result.HasError)
        {
            return result.Errors
                .ToErrorOr<TResult>();
        }

        return factory().ToErrorOr();
    }

    public static ErrorOr<TResult> Then<TValue, TResult>(
        this ErrorOr<TValue> result,
        Func<TValue, TResult> factoryWithValue)
    {
        if (result.HasError)
        {
            return result.Errors
                .ToErrorOr<TResult>();
        }

        return factoryWithValue(result.Value).ToErrorOr();
    }

    public static ErrorOr<TResult> Then<TValue, TResult>(
        this ErrorOr<TValue> errorOr,
        Func<TValue, ErrorOr<TResult>> errorOrFactoryWithValue)
    {
        if (errorOr.HasError)
        {
            return errorOr.Errors
                .ToErrorOr<TResult>();
        }

        return errorOrFactoryWithValue(errorOr.Value);
    }

    // ----- Async
    public static async Task<ErrorOr<TResult>> Then<TValue, TResult>(
        this ErrorOr<TValue> result,
        Func<TValue, Task<ErrorOr<TResult>>> errorOrFactoryWithValueTask)
    {
        if (result.HasError)
        {
            return result.Errors.ToErrorOr<TResult>();
        }

        var resultResult = await errorOrFactoryWithValueTask(result.Value);

        return resultResult;
    }

    public static async Task<ErrorOr<TResult>> Then<TValue, TResult>(
        this Task<ErrorOr<TValue>> errorOrTask,
        Func<TValue, TResult> factoryWithValue)
    {
        var result = await errorOrTask;

        if (result.HasError)
        {
            return result.Errors.ToErrorOr<TResult>();
        }

        return factoryWithValue(result.Value).ToErrorOr();
    }
}