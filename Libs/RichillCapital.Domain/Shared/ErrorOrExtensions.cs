namespace RichillCapital.SharedKernel.Monads;

public static partial class ErrorOrExtensions
{
    public static async Task<ErrorOr<TResult>> Then<TValue, TResult>(
        this ErrorOr<TValue> errorOr,
        Func<TValue, Task<ErrorOr<TResult>>> factory)
    {
        if (errorOr.HasError)
        {
            return errorOr.Errors.ToErrorOr<TResult>();
        }

        var errorOrResult = await factory(errorOr.Value);

        return errorOrResult;
    }

    public static ErrorOr<TResult> Then<TValue, TResult>(
        this ErrorOr<TValue> errorOr,
        Func<TValue, TResult> factory)
    {
        if (errorOr.HasError)
        {
            return errorOr.Errors.ToErrorOr<TResult>();
        }

        var result = factory(errorOr.Value);

        return result.ToErrorOr();
    }

    public static async Task<ErrorOr<TResult>> Then<TValue, TResult>(
        this Task<ErrorOr<TValue>> errorOrTask,
        Func<TValue, TResult> factory)
    {
        var errorOr = await errorOrTask;

        if (errorOr.HasError)
        {
            return errorOr.Errors.ToErrorOr<TResult>();
        }

        var result = factory(errorOr.Value);

        return result.ToErrorOr();
    }

    public static ErrorOr<TResult> Then<TValue, TResult>(
        this ErrorOr<TValue> errorOr,
        Func<TResult> factory)
    {
        if (errorOr.HasError)
        {
            return errorOr.Errors.ToErrorOr<TResult>();
        }

        return factory().ToErrorOr();
    }

    public static ErrorOr<TValue> Then<TValue>(this ErrorOr<TValue> errorOr, Action<TValue> action)
    {
        if (errorOr.HasError)
        {
            return errorOr.Errors.ToErrorOr<TValue>();
        }

        action(errorOr.Value);

        return errorOr;
    }

    public static async Task<TResult> Match<TValue, TResult>(
        this Task<ErrorOr<TValue>> errorOrTask,
        Func<IEnumerable<Error>, TResult> onErrors,
        Func<TValue, TResult> onValue)
    {
        var errorOr = await errorOrTask;

        if (errorOr.HasError)
        {
            return onErrors(errorOr.Errors);
        }

        return onValue(errorOr.Value);
    }
}