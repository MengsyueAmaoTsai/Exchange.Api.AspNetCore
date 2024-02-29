using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.Exchange.Api.Extensions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Trading.ListAccounts;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Exchange.Api.Endpoints.Accounts;

public sealed class List(ISender _sender) : AsyncEndpoint
    .WithRequest<ListAccountsRequest>
    .WithActionResult<IEnumerable<AccountResponse>>
{
    [HttpGet("/api/accounts")]
    [SwaggerOperation(
        Summary = "List accounts",
        Description = "List all accounts.",
        OperationId = "Accounts.List",
        Tags = ["Accounts"])]
    public override async Task<ActionResult<IEnumerable<AccountResponse>>> HandleAsync(
        [FromQuery] ListAccountsRequest request,
        CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();
}

public sealed record class ListAccountsRequest
{
}
