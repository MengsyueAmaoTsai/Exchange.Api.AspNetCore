using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Bots.List;

public sealed record ListBotsQuery() :
    IQuery<Result<IEnumerable<BotDto>>>
{
    public string CacheKey => $"{nameof(ListBotsQuery)}";

    public TimeSpan? CacheDuration => null;
}
