using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.Exchange.Api.Extensions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Trading.ListAccountPositions;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Exchange.Api.Endpoints.Accounts;

public sealed class ListPositions(ISender _sender) : AsyncEndpoint
    .WithRequest<ListAccountPositionsRequest>
    .WithActionResult<IEnumerable<PositionResponse>>
{
    [HttpGet("/api/accounts/{accountId}/positions")]
    [SwaggerOperation(
        Summary = "List account positions.",
        Description = "List all positions for a given account.",
        OperationId = "Accounts.ListPositions",
        Tags = ["Accounts"])]
    public override async Task<ActionResult<IEnumerable<PositionResponse>>> HandleAsync(
        [FromRoute] ListAccountPositionsRequest request,
        CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();
}

public class ListAccountPositionsRequest
{
    [FromRoute(Name = "accountId")]
    public string AccountId { get; init; } = string.Empty;
}