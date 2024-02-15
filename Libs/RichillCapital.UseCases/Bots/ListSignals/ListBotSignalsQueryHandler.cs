using RichillCapital.Domain;
using RichillCapital.Domain.Bots;
using RichillCapital.Domain.Common;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Bots.ListSignals;

internal sealed class ListBotSignalsQueryHandler(
    IReadOnlyRepository<Bot> _botRepository) :
    IQueryHandler<ListBotSignalsQuery, ErrorOr<IEnumerable<SignalDto>>>
{
    public async Task<ErrorOr<IEnumerable<SignalDto>>> Handle(
        ListBotSignalsQuery query,
        CancellationToken cancellationToken)
    {
        var id = BotId.From(query.BotId);

        if (id.IsError)
        {
            return id.Error;
        }

        var bot = await _botRepository.FirstOrDefaultAsync(
            new BotByIdWithSignalsSpecification(id.Value),
            cancellationToken);

        return bot.HasNoValue ?
        DomainErrors.Bots.NotFound(id.Value) :
        bot.Value.Signals
            .Select(signal => signal.ToDto())
            .ToList()
            .AsReadOnly();
    }
}