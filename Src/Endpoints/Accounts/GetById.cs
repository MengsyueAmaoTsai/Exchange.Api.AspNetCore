using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.Exchange.Api.Extensions;
using RichillCapital.SharedKernel.Monads;
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
        CancellationToken cancellationToken = default)
    {
        var query = new GetAccountByIdQuery(request.AccountId);

        var result = await _sender.Send(query, cancellationToken);

        var response = new AccountWithBalancesResponse(
            result.Value.Id,
            result.Value.Name,
            result.Value.PositionMode,
            result.Value.Currency,
            result.Value.Balance
                .Select(balance => new AccountBalanceResponse(
                    balance.Currency,
                    balance.Amount)));

        return ErrorOr<AccountWithBalancesResponse>
            .Is(response)
            .Match(HandleError, Ok);
    }
}

public sealed record class GetAccountByIdRequest
{
    [FromRoute(Name = "accountId")]
    public string AccountId { get; init; } = string.Empty;
}