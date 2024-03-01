using System.ComponentModel.DataAnnotations;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Domain.Bots;
using RichillCapital.Exchange.Api.Common;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Bots.Create;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Exchange.Api.Endpoints.Bots;

public sealed class Create(
    ISender _sender,
    IMapper _mapper) : AsyncEndpoint
    .WithRequest<CreateBotRequest>
    .WithActionResult<CreateBotResponse>
{
    [HttpPost("/api/bots")]
    [SwaggerOperation(
        Summary = "Creates a new bot.",
        Description = "Creates a new bot.",
        OperationId = "Bots.Create",
        Tags = ["Bots"])]
    public override async Task<ActionResult<CreateBotResponse>> HandleAsync(
        [FromBody] CreateBotRequest request,
        CancellationToken cancellationToken = default) =>
        await request
            .ToErrorOr()
            .Then(MapToCommand)
            .Then(command => _sender.Send(command, cancellationToken))
            .Then(MapToResponse)
            .Match(HandleError, Ok);

    private CreateBotCommand MapToCommand(CreateBotRequest request) => _mapper.Map<CreateBotCommand>(request);

    private CreateBotResponse MapToResponse(BotId id) => _mapper.Map<CreateBotResponse>(id);
}

public sealed record class CreateBotRequest
{
    [Required]
    public string Id { get; init; } = string.Empty;

    [Required]
    public string Name { get; init; } = string.Empty;

    [Required]
    public string Description { get; init; } = string.Empty;

    [Required]
    public string Side { get; init; } = string.Empty;

    [Required]
    public string Platform { get; init; } = string.Empty;
}

public sealed record CreateBotResponse(string BotId);
