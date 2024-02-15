namespace RichillCapital.UseCases.Common;

public interface ICachedQuery<TResult> :
    IQuery<TResult>
{
    string CacheKey { get; }

    TimeSpan? CacheDuration { get; }
}