using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.Exchange.Api.Extensions;
using RichillCapital.UseCases.Trading.GetAccountPerformance;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.ExchangeApi.Endpoints.Accounts;

public sealed class GetAccountPerformance(ISender _sender) : AsyncEndpoint
    .WithRequest<GetAccountPerformanceRequest>
    .WithActionResult<AccountPerformanceResponse>
{
    [HttpGet("/api/accounts/{accountId}/performance")]
    [SwaggerOperation(
        Summary = "Get account performance",
        Description = "Get account performance metrics",
        OperationId = "Accounts.GetAccountPerformance",
        Tags = ["Accounts"])]
    public override async Task<ActionResult<AccountPerformanceResponse>> HandleAsync(
        [FromRoute] GetAccountPerformanceRequest request,
        CancellationToken cancellationToken = default) =>
        (await _sender.Send(new GetAccountPerformanceQuery(request.AccountId), cancellationToken))
            .Map(performance => new AccountPerformanceResponse(
                new AccountPerformanceMetricsResponse(
                    decimal.Zero,
                    decimal.Zero,
                    decimal.Zero)))
            .Match(HandleError, Ok);
}

public sealed record class GetAccountPerformanceRequest
{
    [FromRoute(Name = "accountId")]
    public string AccountId { get; init; } = string.Empty;
}