using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.Exchange.Api.Extensions;
using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Bots.List;

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
    public override async Task<ActionResult<IEnumerable<BotResponse>>> HandleAsync(
        [FromQuery] ListBotsRequest request,
        CancellationToken cancellationToken = default) =>
        (await _sender.Send(new ListBotsQuery(), cancellationToken))
            .Map(bots => bots
                .Select(bot => new BotResponse(
                    bot.Id,
                    bot.Name,
                    bot.Description,
                    bot.Platform)))
            .Match(Ok, HandleError);
}

public sealed record class ListBotsRequest
{
}
