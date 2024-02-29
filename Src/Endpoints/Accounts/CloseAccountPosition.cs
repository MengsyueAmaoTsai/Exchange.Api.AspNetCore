using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Exchange.Api.Endpoints.Accounts;

public sealed class CloseAccountPosition(ISender _sender) : AsyncEndpoint
    .WithRequest<ClosePositionRequest>
    .WithActionResult<ClosePositionResponse>
{
    [HttpGet("/api/accounts/{accountId}/positions/{positionId}/close")]
    [SwaggerOperation(
        Summary = "Close an account position",
        Description = "Close an account position",
        OperationId = "Accounts.CloseAccountPosition",
        Tags = ["Accounts"])]
    public override async Task<ActionResult<ClosePositionResponse>> HandleAsync(
        [FromRoute] ClosePositionRequest request,
        CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();
}

public sealed record class ClosePositionRequest
{
    [FromRoute(Name = "accountId")]
    public string AccountId { get; init; } = string.Empty;

    [FromRoute(Name = "positionId")]
    public string PositionId { get; init; } = string.Empty;
}

public sealed record ClosePositionResponse(string PositionId);