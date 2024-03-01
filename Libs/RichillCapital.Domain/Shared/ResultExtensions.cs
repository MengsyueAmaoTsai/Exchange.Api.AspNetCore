namespace RichillCapital.SharedKernel.Monads;

public static partial class ResultExtensions
{
    public static Result<TResult> Then<TValue, TResult>(
        this Result<TValue> result,
        Func<TValue, TResult> factory)
    {
        if (result.IsFailure)
        {
            return result.Error.ToResult<TResult>();
        }

        return factory(result.Value).ToResult();
    }

    public static Result<TValue> Ensure<TValue>(
        this Result<TValue> result,
        Func<TValue, bool> ensure,
        Error error)
    {
        if (result.IsFailure)
        {
            return result.Error.ToResult<TValue>();
        }

        if (!ensure(result.Value))
        {
            return error.ToResult<TValue>();
        }

        return result;
    }
}