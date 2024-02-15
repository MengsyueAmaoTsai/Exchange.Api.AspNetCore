using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Bots.ListSignals;

public sealed record ListBotSignalsQuery(
    string BotId) :
    ICachedQuery<ErrorOr<IEnumerable<SignalDto>>>
{
    public string CacheKey => $"{nameof(ListBotSignalsQuery)}-{BotId}";

    public TimeSpan? CacheDuration => null;
}
