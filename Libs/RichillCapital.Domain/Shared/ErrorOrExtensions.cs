namespace RichillCapital.SharedKernel.Monads;

public static class ErrorOrExtensions
{
    public static TResult Match<TSource, TResult>(
        this ErrorOr<TSource> errorOrSource,
        Func<IEnumerable<Error>, TResult> onIsError,
        Func<TSource, TResult> onIsValue) =>
        errorOrSource.IsError ?
            onIsError(errorOrSource.Errors) :
            onIsValue(errorOrSource.Value);
}