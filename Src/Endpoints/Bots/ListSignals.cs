using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.Exchange.Api.Extensions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Bots.ListSignals;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Exchange.Api.Endpoints.Bots;

public sealed class ListSignals(ISender _sender) : AsyncEndpoint
    .WithRequest<ListBotSignalsRequest>
    .WithActionResult<IEnumerable<SignalResponse>>
{
    [HttpGet("/api/bots/{botId}/signals")]
    [SwaggerOperation(
        Summary = "List bot signals.",
        Description = "List bot signals.",
        OperationId = "Bots.ListSignals",
        Tags = ["Bots"])]
    public override async Task<ActionResult<IEnumerable<SignalResponse>>> HandleAsync(
        [FromRoute] ListBotSignalsRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = new ListBotSignalsQuery(request.BotId);

        var result = await _sender.Send(query, cancellationToken);

        var response = result.Value
            .Select(signal => new SignalResponse(
                signal.Time,
                signal.TradeType,
                signal.Symbol,
                signal.Volume,
                signal.Price));

        return Result
            .Success(response)
            .Match(Ok, HandleError);
    }
}

public sealed record class ListBotSignalsRequest
{
    [FromRoute(Name = "botId")]
    public string BotId { get; init; } = string.Empty;
}