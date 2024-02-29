using System.ComponentModel.DataAnnotations;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Domain.Bots;
using RichillCapital.Exchange.Api.Common;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Bots.EmitSignal;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Exchange.Api.Endpoints.Bots;

public sealed class EmitSignal(
    ISender _sender,
    IMapper _mapper) : AsyncEndpoint
    .WithRequest<EmitSignalRequest>
    .WithActionResult<EmitSignalResponse>
{
    [HttpPost("/api/bots/{botId}/signals")]
    [SwaggerOperation(
        Summary = "Emit signal.",
        Description = "Emit signal.",
        OperationId = "Bots.EmitSignal",
        Tags = ["Bots"])]
    public override async Task<ActionResult<EmitSignalResponse>> HandleAsync(
        [FromRoute] EmitSignalRequest request,
        CancellationToken cancellationToken = default) =>
        await ErrorOr<EmitSignalRequest>.Is(request)
            .Then(ToCommand)
            .Then(command => _sender.Send(command, cancellationToken))
            .Then(ToResponse)
            .Match(HandleError, Ok);

    private EmitBotSignalCommand ToCommand(EmitSignalRequest request) =>
        _mapper.Map<EmitBotSignalCommand>(request);

    private EmitSignalResponse ToResponse(BotId botId) =>
        _mapper.Map<EmitSignalResponse>(botId);
}

public sealed record EmitSignalResponse(string BotId);

public sealed record class EmitSignalRequest
{
    [FromRoute(Name = "botId")]
    public string BotId { get; init; } = string.Empty;

    [FromBody]
    public EmitSignalRequestBody Body { get; init; } = new EmitSignalRequestBody();
}

public sealed record class EmitSignalRequestBody
{
    [Required]
    public DateTimeOffset Time { get; init; }

    [Required]
    public string TradeType { get; init; } = string.Empty;

    [Required]
    public string Symbol { get; init; } = string.Empty;

    [Required]
    public decimal Volume { get; init; }

    [Required]
    public decimal Price { get; init; }
}