using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Bots.ListSignals;

public sealed record ListBotSignalsQuery(
    string BotId) :
    IQuery<ErrorOr<IEnumerable<SignalDto>>>
{
    public string CacheKey => $"{nameof(ListBotSignalsQuery)}-{BotId}";

    public TimeSpan? CacheDuration => null;
}
