namespace RichillCapital.SharedKernel.Monad;

public static class ResultExtensions
{
    public static Result<TDestination> Map<TSource, TDestination>(
        this Result<TSource> result,
        Func<TSource, TDestination> map) =>
        result.IsSuccess ? map(result.Value) : result.Error;
}