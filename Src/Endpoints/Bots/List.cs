using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.Exchange.Api.Extensions;
using RichillCapital.SharedKernel.Monads;
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
        CancellationToken cancellationToken = default)
    {
        var query = new ListBotsQuery();

        var botsResult = await _sender.Send(query, cancellationToken);

        var response = botsResult.Value
            .Select(bot => new BotResponse(
                bot.Id,
                bot.Name,
                bot.Description,
                bot.Platform));

        return Result<IEnumerable<BotResponse>>
            .Success(response)
            .Match(Ok, HandleError);
    }
}

public sealed record class ListBotsRequest
{
}
