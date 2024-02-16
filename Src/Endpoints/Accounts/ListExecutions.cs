using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.Exchange.Api.Extensions;
using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Trading.ListAccountExecutions;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Exchange.Api.Endpoints.Accounts;

public sealed class ListExecutions(ISender _sender) : AsyncEndpoint
    .WithRequest<ListExecutionsRequest>
    .WithActionResult<IEnumerable<ExecutionResponse>>
{
    [HttpGet("/api/accounts/{accountId}/executions")]
    [SwaggerOperation(
        Summary = "List account executions.",
        Description = "List all executions for a given account.",
        OperationId = "Accounts.ListExecutions",
        Tags = ["Accounts"])]
    public override async Task<ActionResult<IEnumerable<ExecutionResponse>>> HandleAsync(
        [FromRoute] ListExecutionsRequest request,
        CancellationToken cancellationToken = default) =>
        (await _sender.Send(
            new ListAccountExecutionsQuery(request.AccountId),
            cancellationToken))
            .Map(executions => executions
                .Select(execution => new ExecutionResponse(
                    execution.Time,
                    execution.TradeType,
                    execution.Symbol,
                    execution.Quantity,
                    execution.Price,
                    execution.Commission,
                    execution.Tax)))
            .Match(HandleError, Ok);
}

public sealed record class ListExecutionsRequest
{
    [FromRoute(Name = "accountId")]
    public string AccountId { get; init; } = string.Empty;
}