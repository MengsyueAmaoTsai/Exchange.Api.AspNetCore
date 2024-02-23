using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.Exchange.Api.Extensions;
using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Trading.ListAccountTrades;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Exchange.Api.Endpoints.Accounts;

public sealed class ListTrades(ISender _sender) : AsyncEndpoint
    .WithRequest<ListAccountTradesRequest>
    .WithActionResult<IEnumerable<TradeResponse>>
{
    [HttpGet("/api/accounts/{accountId}/trades")]
    [SwaggerOperation(
        Summary = "List account trades.",
        Description = "List all trades for a given account.",
        OperationId = "Accounts.ListTrades",
        Tags = ["Accounts"])]
    public override async Task<ActionResult<IEnumerable<TradeResponse>>> HandleAsync(
        [FromRoute] ListAccountTradesRequest request,
        CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();
}

public sealed record class ListAccountTradesRequest
{
    [FromRoute(Name = "accountId")]
    public string AccountId { get; init; } = string.Empty;
}