using RichillCapital.Domain;
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
        CancellationToken cancellationToken)
    {
        var id = BotId.From(query.BotId);

        if (id.IsFailure)
        {
            return id.Error
                .ToErrorOr<BotDto>();
        }

        var bot = await _botRepository.GetByIdAsync(id.Value, cancellationToken);

        return bot.HasNoValue ?
            DomainErrors.Bots.NotFound(id.Value).ToErrorOr<BotDto>() :
            BotDto.From(bot.Value).ToErrorOr();
    }
}