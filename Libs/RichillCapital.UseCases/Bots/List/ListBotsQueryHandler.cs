using RichillCapital.Domain.Bots;
using RichillCapital.Domain.Common;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Bots.List;

internal sealed class ListBotsQueryHandler(
    IReadOnlyRepository<Bot> _botRepository) :
    IQueryHandler<ListBotsQuery, Result<IEnumerable<BotDto>>>
{
    public async Task<Result<IEnumerable<BotDto>>> Handle(
        ListBotsQuery query,
        CancellationToken cancellationToken)
    {
        var botsResult = await _botRepository.ListAsync(cancellationToken).ToResult();

        if (botsResult.IsFailure)
        {
            return botsResult.Error.ToResult<IEnumerable<BotDto>>();
        }

        return botsResult.Value.Select(BotDto.From).ToResult();
    }
}