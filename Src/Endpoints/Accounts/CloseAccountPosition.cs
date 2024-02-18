using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.Exchange.Api.Extensions;
using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Trading.ClosePosition;

namespace RichillCapital.Exchange.Api.Endpoints.Accounts;

public sealed class CloseAccountPosition(ISender _sender) : AsyncEndpoint
    .WithRequest<ClosePositionRequest>
    .WithActionResult<ClosePositionResponse>
{
    [HttpGet("/api/accounts/{accountId}/positions/{positionId}/close")]
    public override async Task<ActionResult<ClosePositionResponse>> HandleAsync(
        [FromRoute] ClosePositionRequest request,
        CancellationToken cancellationToken = default) =>
        (await _sender.Send(
            new ClosePositionCommand(request.PositionId),
            cancellationToken))
            .Map(id => new ClosePositionResponse(id.Value))
            .Match(HandleError, Ok);
}

public sealed record class ClosePositionRequest
{
    [FromRoute(Name = "accountId")]
    public string AccountId { get; init; } = string.Empty;

    [FromRoute(Name = "positionId")]
    public string PositionId { get; init; } = string.Empty;
}

public sealed record ClosePositionResponse(string PositionId);