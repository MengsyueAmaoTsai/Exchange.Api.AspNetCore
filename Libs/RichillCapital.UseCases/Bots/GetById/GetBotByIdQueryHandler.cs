using RichillCapital.Domain;
using RichillCapital.Domain.Bots;
using RichillCapital.Domain.Common;
using RichillCapital.SharedKernel;
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
        Result<BotId> monad = query.BotId
            .ToResult()
            .Then(BotId.From);

        // id.ToResult().Ensure(Exists, NotFound).Then(BotDto.From).ToErrorOr();
        var idResult = BotId.From(query.BotId);

        if (idResult.IsFailure)
        {
            return idResult.Error
                .ToErrorOr<BotDto>();
        }

        var maybeBot = await _botRepository.GetByIdAsync(idResult.Value, cancellationToken);

        if (maybeBot.IsNull)
        {
            return NotFound(idResult.Value);
        }

        return BotDto.From(maybeBot.Value).ToErrorOr();
    }

    private static ErrorOr<BotDto> NotFound(BotId id) =>
        Error.NotFound($"Bot with id '{id}' was not found.")
            .ToErrorOr<BotDto>();
}