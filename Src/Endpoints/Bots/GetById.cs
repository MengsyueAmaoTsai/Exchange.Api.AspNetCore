using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Bots;
using RichillCapital.UseCases.Bots.GetById;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Exchange.Api.Endpoints.Bots;

public sealed class GetById(
    ISender _sender,
    IMapper _mapper) : AsyncEndpoint
    .WithRequest<GetBotByIdRequest>
    .WithActionResult<BotResponse>
{
    [HttpGet("/api/bots/{botId}")]
    [SwaggerOperation(
        Summary = "Gets a bot by its ID.",
        Description = "Gets a bot by its ID.",
        OperationId = "Bots.GetById",
        Tags = ["Bots"])]
    public override async Task<ActionResult<BotResponse>> HandleAsync(
        [FromRoute] GetBotByIdRequest request,
        CancellationToken cancellationToken = default) =>
        await request
            .ToErrorOr()
            .Then(MapToQuery)
            .Then(query => _sender.Send(query, cancellationToken))
            .Then(MapToResponse)
            .Match(HandleError, Ok);

    private GetBotByIdQuery MapToQuery(GetBotByIdRequest request) => _mapper.Map<GetBotByIdQuery>(request);

    private BotResponse MapToResponse(BotDto bot) => _mapper.Map<BotResponse>(bot);
}

public sealed record class GetBotByIdRequest
{
    [FromRoute(Name = "botId")]
    public string BotId { get; init; } = string.Empty;
}