namespace RichillCapital.SharedKernel.Monad;

public static class ErrorOrExtensions
{
    public static ErrorOr<TValue> Ensure<TValue>(
        this ErrorOr<TValue> errorOr,
        Func<TValue, bool> predicate,
        Error error) =>
        errorOr.IsError ?
            errorOr.Error :
            predicate(errorOr.Value) ? errorOr.Value : error;

    public static ErrorOr<TDestination> Map<TSource, TDestination>(
        this ErrorOr<TSource> errorOr,
        Func<TSource, TDestination> map) =>
        errorOr.IsError ?
            errorOr.Error :
            map(errorOr.Value);
}