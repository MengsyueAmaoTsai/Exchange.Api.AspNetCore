using RichillCapital.Domain;
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
        CancellationToken cancellationToken)
    {
        var id = BotId.From(query.BotId);

        if (id.IsFailure)
        {
            return id.Error
                .ToErrorOr<IEnumerable<SignalDto>>();
        }

        var bot = await _botRepository.FirstOrDefaultAsync(
            new BotByIdWithSignalsSpecification(id.Value),
            cancellationToken);

        return bot.HasNoValue ?
        DomainErrors.Bots.NotFound(id.Value).ToErrorOr<IEnumerable<SignalDto>>() :
        bot.Value.Signals
            .Select(SignalDto.From)
            .ToList()
            .AsReadOnly()
            .ToErrorOr<IEnumerable<SignalDto>>();
    }
}