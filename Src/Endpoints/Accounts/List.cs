using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.Exchange.Api.Extensions;
using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Trading.ListAccounts;

namespace RichillCapital.Exchange.Api.Endpoints.Accounts;

public sealed class List(ISender _sender) : AsyncEndpoint
    .WithRequest<ListAccountsRequest>
    .WithActionResult<IEnumerable<AccountResponse>>
{
    public override async Task<ActionResult<IEnumerable<AccountResponse>>> HandleAsync(
        [FromQuery] ListAccountsRequest request,
        CancellationToken cancellationToken = default) =>
        (await _sender.Send(new ListAccountsQuery(), cancellationToken))
            .Map(accounts => accounts
                .Select(account => new AccountResponse(
                    account.Id,
                    account.Name,
                    account.Currency)))
            .Match(Ok, HandleError);
}

public sealed record class ListAccountsRequest
{
}
