using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.Exchange.Api.Extensions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Bots.GetById;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Exchange.Api.Endpoints.Bots;

public sealed class GetById(ISender _sender) : AsyncEndpoint
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
        CancellationToken cancellationToken = default)
    {
        var query = new GetBotByIdQuery(request.BotId);

        var result = await _sender.Send(query, cancellationToken);

        var response = new BotResponse(
            result.Value.Id,
            result.Value.Name,
            result.Value.Description,
            result.Value.Platform);

        return ErrorOr<BotResponse>
            .Is(response)
            .Match(HandleError, Ok);
    }
}

public sealed record class GetBotByIdRequest
{
    [FromRoute(Name = "botId")]
    public string BotId { get; init; } = string.Empty;
}