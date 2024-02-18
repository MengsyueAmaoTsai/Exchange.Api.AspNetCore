using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.Exchange.Api.Extensions;
using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Trading.ListAccountOrders;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Exchange.Api.Endpoints.Accounts;

public sealed class ListOrders(ISender _sender) : AsyncEndpoint
    .WithRequest<ListOrdersRequest>
    .WithActionResult<IEnumerable<OrderResponse>>
{
    [HttpGet("/api/accounts/{accountId}/orders")]
    [SwaggerOperation(
        Summary = "List account orders.",
        Description = "List all orders for a given account.",
        OperationId = "Accounts.ListOrders",
        Tags = ["Accounts"])]
    public override async Task<ActionResult<IEnumerable<OrderResponse>>> HandleAsync(
        [FromRoute] ListOrdersRequest request,
        CancellationToken cancellationToken = default) =>
        (await _sender.Send(new ListAccountOrdersQuery(request.AccountId), cancellationToken))
            .Map(orders => orders
                .Select(order => new OrderResponse(
                    order.Id,
                    order.Time,
                    order.TradeType,
                    order.Quantity,
                    order.RemainingQuantity,
                    order.Symbol,
                    order.Type,
                    order.TimeInForce,
                    order.Status)))
            .Match(HandleError, Ok);
}

public sealed record class ListOrdersRequest
{
    [FromRoute(Name = "accountId")]
    public string AccountId { get; init; } = string.Empty;
}