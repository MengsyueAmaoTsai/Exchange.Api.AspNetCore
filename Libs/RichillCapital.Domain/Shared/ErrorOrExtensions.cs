using System.Formats.Asn1;

namespace RichillCapital.SharedKernel.Monads;

public static partial class ErrorOrExtensions
{
    public static ErrorOr<TValue> Then<TValue>(this ErrorOr<TValue> errorOr, Action<TValue> action)
    {
        if (errorOr.HasError)
        {
            return errorOr.Errors.ToErrorOr<TValue>();
        }

        action(errorOr.Value);

        return errorOr;
    }

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

    public static async Task<ErrorOr<TValue>> Then<TValue>(this ErrorOr<TValue> errorOr, Func<Task> task)
    {
        if (errorOr.HasError)
        {
            return errorOr.Errors.ToErrorOr<TValue>();
        }

        await task();

        return errorOr.Value.ToErrorOr();
    }
}