using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.Exchange.Api.Extensions;
using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Trading.CreateAccount;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Exchange.Api.Endpoints.Accounts;

public sealed class Create(ISender _sender) : AsyncEndpoint
    .WithRequest<CreateAccountRequest>
    .WithActionResult<CreateAccountResponse>
{
    [HttpPost("/api/accounts")]
    [SwaggerOperation(
        Summary = "Creates a new account.",
        Description = "Creates a new account.",
        OperationId = "Accounts.Create",
        Tags = ["Accounts"])]
    public override async Task<ActionResult<CreateAccountResponse>> HandleAsync(
        [FromBody] CreateAccountRequest request,
        CancellationToken cancellationToken = default) =>
        (await _sender.Send(new CreateAccountCommand(request.Name, request.Currency), cancellationToken))
            .Map(id => new CreateAccountResponse(id.Value))
            .Match(HandleError, Ok);
}

public sealed record class CreateAccountRequest
{
    public string Name { get; init; } = string.Empty;

    public string Currency { get; init; } = string.Empty;
}

public sealed record CreateAccountResponse(string AccountId);

