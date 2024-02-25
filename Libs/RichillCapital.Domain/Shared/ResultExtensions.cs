namespace RichillCapital.SharedKernel.Monads;

public static class ResultExtensions
{
    public static TResult Match<TSource, TResult>(
        this Result<TSource> sourceResult,
        Func<TSource, TResult> onSuccess,
        Func<Error, TResult> onFailure) =>
        sourceResult.IsFailure ?
            onFailure(sourceResult.Error) :
            onSuccess(sourceResult.Value);
}