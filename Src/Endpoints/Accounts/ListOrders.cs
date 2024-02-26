using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.Exchange.Api.Extensions;
using RichillCapital.SharedKernel.Monads;
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
        CancellationToken cancellationToken = default)
    {
        var query = new ListAccountOrdersQuery(request.AccountId);

        var ordersResult = await _sender.Send(query, cancellationToken);

        var response = ordersResult.Value
            .Select(order => new OrderResponse(
                order.Id,
                order.Time,
                order.TradeType,
                order.Quantity,
                order.RemainingQuantity,
                order.Symbol,
                order.Type,
                order.TimeInForce,
                order.Status));

        return Result<IEnumerable<OrderResponse>>
            .Success(response)
            .Match(Ok, HandleError);
    }
}

public sealed record class ListOrdersRequest
{
    [FromRoute(Name = "accountId")]
    public string AccountId { get; init; } = string.Empty;
}