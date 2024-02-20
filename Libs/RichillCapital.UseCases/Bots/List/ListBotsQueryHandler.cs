using RichillCapital.Domain.Bots;
using RichillCapital.Domain.Common;
using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Bots.List;

internal sealed class ListBotsQueryHandler(
    IReadOnlyRepository<Bot> _botRepository) :
    IQueryHandler<ListBotsQuery, Result<IEnumerable<BotDto>>>
{
    public async Task<Result<IEnumerable<BotDto>>> Handle(
        ListBotsQuery query,
        CancellationToken cancellationToken) =>
        (await _botRepository.ListAsync(cancellationToken))
            .Select(BotDto.From)
            .ToList()
            .AsReadOnly();
}