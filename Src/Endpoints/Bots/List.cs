using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Exchange.Api.Endpoints.Bots;

public sealed class List(ISender _sender) : AsyncEndpoint
    .WithRequest<ListBotsRequest>
    .WithActionResult<IEnumerable<BotResponse>>
{
    [HttpGet("/api/bots")]
    [SwaggerOperation(
        Summary = "Lists all bots.",
        Description = "Lists all bots.",
        OperationId = "Bots.List",
        Tags = ["Bots"])]
    public override Task<ActionResult<IEnumerable<BotResponse>>> HandleAsync(
        [FromQuery] ListBotsRequest request,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

public sealed record class ListBotsRequest
{
}
