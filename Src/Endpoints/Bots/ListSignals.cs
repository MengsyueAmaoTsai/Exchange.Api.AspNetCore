using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Bots;
using RichillCapital.UseCases.Bots.ListSignals;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Exchange.Api.Endpoints.Bots;

public sealed class ListSignals(
    ISender _sender,
    IMapper _mapper) : AsyncEndpoint
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
        CancellationToken cancellationToken = default) =>
        await request
            .ToErrorOr()
            .Then(MapToQuery)
            .Then(query => _sender.Send(query, cancellationToken))
            .Then(MapToResponse)
            .Match(HandleError, Ok);

    private ListBotSignalsQuery MapToQuery(ListBotSignalsRequest request) =>
        _mapper.Map<ListBotSignalsQuery>(request);

    private IEnumerable<SignalResponse> MapToResponse(IEnumerable<SignalDto> signals) =>
        _mapper.Map<IEnumerable<SignalResponse>>(signals);
}

public sealed record class ListBotSignalsRequest
{
    [FromRoute(Name = "botId")]
    public string BotId { get; init; } = string.Empty;
}