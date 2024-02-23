using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.Exchange.Api.Extensions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Trading.CreateAccountOrder;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Exchange.Api.Endpoints.Accounts;

public sealed class CreateAccountOrder(ISender _sender) : AsyncEndpoint
    .WithRequest<CreateAccountOrderRequest>
    .WithActionResult<CreateAccountOrderResponse>
{
    [HttpPost("/api/accounts/{accountId}/orders")]
    [SwaggerOperation(
        Summary = "Create an account order",
        Description = "Create an account order",
        OperationId = "Accounts.CreateAccountOrder",
        Tags = ["Accounts"])]
    public override async Task<ActionResult<CreateAccountOrderResponse>> HandleAsync(
        [FromRoute] CreateAccountOrderRequest request,
        CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();
}

public sealed record class CreateAccountOrderRequest
{
    [FromRoute(Name = "accountId")]
    public string AccountId { get; init; } = string.Empty;

    [FromBody]
    public CreateAccountOrderRequestBody Body { get; init; } = new();
}


public sealed record class CreateAccountOrderRequestBody
{
    public string TradeType { get; init; } = string.Empty;

    public decimal Quantity { get; init; }

    public string Symbol { get; init; } = string.Empty;

    public string OrderType { get; init; } = string.Empty;

    public string TimeInForce { get; init; } = string.Empty;
}

public sealed record CreateAccountOrderResponse(string OrderId);