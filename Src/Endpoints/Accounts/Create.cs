using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;

namespace RichillCapital.Exchange.Api.Endpoints.Accounts;

public sealed class Create(ISender _sender) : AsyncEndpoint
    .WithRequest<CreateAccountRequest>
    .WithActionResult<CreateAccountResponse>
{
    [HttpPost("/api/accounts")]
    public override Task<ActionResult<CreateAccountResponse>> HandleAsync(
        [FromBody] CreateAccountRequest request,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

public sealed record class CreateAccountRequest
{
    public string Name { get; init; } = string.Empty;

    public string Currency { get; init; } = string.Empty;
}

public sealed record CreateAccountResponse(string AccountId);

