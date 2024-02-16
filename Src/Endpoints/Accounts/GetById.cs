using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.Exchange.Api.Extensions;
using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Trading.GetAccountById;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Exchange.Api.Endpoints.Accounts;

public sealed class GetById(ISender _sender) : AsyncEndpoint
    .WithRequest<GetAccountByIdRequest>
    .WithActionResult<AccountWithBalancesResponse>
{
    [HttpGet("/api/accounts/{accountId}")]
    [SwaggerOperation(
        Summary = "Retrieve an account by its unique identifier.",
        Description = "Retrieves an account by its unique identifier.",
        OperationId = "Accounts.GetById",
        Tags = ["Accounts"])]
    public override async Task<ActionResult<AccountWithBalancesResponse>> HandleAsync(
        [FromRoute] GetAccountByIdRequest request,
        CancellationToken cancellationToken = default) =>
        (await _sender.Send(new GetAccountByIdQuery(request.AccountId), cancellationToken))
            .Map(account => new AccountWithBalancesResponse(
                account.Id,
                account.Name,
                account.PositionMode,
                account.Currency,
                account.Balance
                    .Select(balance => new AccountBalanceResponse(
                        balance.Currency,
                        balance.Amount))))
            .Match(HandleError, Ok);
}

public sealed record class GetAccountByIdRequest
{
    [FromRoute(Name = "accountId")]
    public string AccountId { get; init; } = string.Empty;
}