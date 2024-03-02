namespace RichillCapital.SharedKernel.Monads;

public static partial class ErrorOrExtensions
{
    // ----- Async
    public static async Task<ErrorOr<TResult>> Then<TValue, TResult>(
        this ErrorOr<TValue> errorOr,
        Func<TValue, Task<Maybe<TResult>>> maybeFactoryWithValueTask,
        Func<TValue, Error> errorFactoryWithValue)
    {
        if (errorOr.HasError)
        {
            return errorOr.Errors.ToErrorOr<TResult>();
        }

        var maybe = await maybeFactoryWithValueTask(errorOr.Value);

        if (maybe.IsNull)
        {
            return errorFactoryWithValue(errorOr.Value).ToErrorOr<TResult>();
        }

        return maybe.Value.ToErrorOr();
    }
}