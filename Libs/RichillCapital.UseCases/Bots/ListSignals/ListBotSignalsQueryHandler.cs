using RichillCapital.Domain.Bots;
using RichillCapital.Domain.Common;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Bots.ListSignals;

internal sealed class ListBotSignalsQueryHandler(
    IReadOnlyRepository<Bot> _botRepository) :
    IQueryHandler<ListBotSignalsQuery, ErrorOr<IEnumerable<SignalDto>>>
{
    public async Task<ErrorOr<IEnumerable<SignalDto>>> Handle(
        ListBotSignalsQuery query,
        CancellationToken cancellationToken) =>
        await query.BotId
            .ToResult()
            .Then(BotId.From)
            .Then(id => new BotByIdWithSignalsSpecification(id))
            .Then(
                spec => _botRepository.FirstOrDefaultAsync(spec, cancellationToken),
                spec => BotErrors.NotFound(spec.BotId))
            .Then(bot => bot.Signals
                .Select(SignalDto.From))
            .ToErrorOr();
}