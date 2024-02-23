using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.Exchange.Api.Extensions;
using RichillCapital.SharedKernel.Monad;
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
        CancellationToken cancellationToken = default) => throw new NotImplementedException();
    // (await _sender.Send(new ListBotSignalsQuery(request.BotId), cancellationToken))
    //     .Map(signals => signals
    //         .Select(signal => new SignalResponse(
    //             signal.Time,
    //             signal.TradeType,
    //             signal.Symbol,
    //             signal.Volume,
    //             signal.Price)))
    //     .Match(HandleError, Ok);
}

public sealed record class ListBotSignalsRequest
{
    [FromRoute(Name = "botId")]
    public string BotId { get; init; } = string.Empty;
}