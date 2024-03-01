namespace RichillCapital.SharedKernel.Monads;

public static partial class ErrorOrExtensions
{
    public static ErrorOr<TResult> Then<TValue, TResult>(
        this ErrorOr<TValue> errorOr,
        Func<TValue, TResult> factory)
    {
        if (errorOr.HasError)
        {
            return errorOr.Errors.ToErrorOr<TResult>();
        }

        return factory(errorOr.Value).ToErrorOr();
    }

    public static async Task<ErrorOr<TResult>> Then<TValue, TResult>(
        this ErrorOr<TValue> errorOr,
        Func<TValue, Task<ErrorOr<TResult>>> errorOrFactory)
    {
        if (errorOr.HasError)
        {
            return errorOr.Errors.ToErrorOr<TResult>();
        }

        return await errorOrFactory(errorOr.Value);
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

        return factory(errorOr.Value).ToErrorOr();
    }
}