using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Domain.Trading;
using RichillCapital.Exchange.Api.Common;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Trading.CreateAccount;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Exchange.Api.Endpoints.Accounts;

public sealed class Create(
    ISender _sender,
    IMapper _mapper) : AsyncEndpoint
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
        await request
            .ToErrorOr()
            .Then(MapToCommand)
            .Then(command => _sender.Send(command, cancellationToken))
            .Then(MapToResponse)
            .Match(HandleError, Ok);

    private CreateAccountCommand MapToCommand(CreateAccountRequest request) =>
        _mapper.Map<CreateAccountCommand>(request);

    private CreateAccountResponse MapToResponse(AccountId id) =>
        _mapper.Map<CreateAccountResponse>(id);
}

public sealed record class CreateAccountRequest
{
    public string Name { get; init; } = string.Empty;

    public string PositionMode { get; init; } = string.Empty;

    public string Environment { get; init; } = string.Empty;

    public string Currency { get; init; } = string.Empty;

    public decimal InitialDeposit { get; init; }
}

public sealed record CreateAccountResponse(string AccountId);
