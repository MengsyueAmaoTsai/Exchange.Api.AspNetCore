using RichillCapital.Domain.Bots;
using RichillCapital.Domain.Common;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Bots.GetById;

internal sealed class GetBotByIdQueryHandler(
    IReadOnlyRepository<Bot> _botRepository) :
    IQueryHandler<GetBotByIdQuery, ErrorOr<BotDto>>
{
    public async Task<ErrorOr<BotDto>> Handle(
        GetBotByIdQuery query,
        CancellationToken cancellationToken) =>
        await query.BotId
            .ToResult()
            .Then(BotId.From)
            .Then(GetBotAsync, BotErrors.NotFound)
            .Then(BotDto.From)
            .ToErrorOr();

    private async Task<Maybe<Bot>> GetBotAsync(BotId id) =>
        await _botRepository.GetByIdAsync(id, default);
}