using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;

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
    public override Task<ActionResult<BotResponse>> HandleAsync(
        [FromRoute] GetBotByIdRequest request,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

public sealed record class GetBotByIdRequest
{
    [FromRoute(Name = "botId")]
    public string BotId { get; init; } = string.Empty;
}