using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.Exchange.Api.Extensions;
using RichillCapital.SharedKernel.Monad;
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
        (await _sender.Send(new ListAccountsQuery(), cancellationToken))
            .Map(accounts => accounts
                .Select(account => new AccountResponse(
                    account.Id,
                    account.Name,
                    account.PositionMode,
                    account.Currency)))
            .Match(Ok, HandleError);
}

public sealed record class ListAccountsRequest
{
}
