namespace RichillCapital.SharedKernel.Monads;

public static partial class ResultExtensions
{
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

        return maybe.ToResult(errorFactoryWithValue(result.Value));
    }

    public static async Task<Result<TResult>> Then<TValue, TResult>(
        this Result<TValue> result,
        Func<TValue, Task<ErrorOr<TResult>>> errorOrFactoryWithValueTask)
    {
        if (result.IsFailure)
        {
            return result.Error.ToResult<TResult>();
        }

        var errorOr = await errorOrFactoryWithValueTask(result.Value);

        return errorOr.ToResult();
    }
}