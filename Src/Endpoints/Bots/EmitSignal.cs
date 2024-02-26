using System.ComponentModel.DataAnnotations;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.Exchange.Api.Extensions;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Bots.EmitSignal;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Exchange.Api.Endpoints.Bots;

public sealed class EmitSignal(ISender _sender) : AsyncEndpoint
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
        CancellationToken cancellationToken = default)
    {
        var command = new EmitBotSignalCommand(
            request.Body.Time,
            request.Body.TradeType,
            request.Body.Symbol,
            request.Body.Volume,
            request.Body.Price,
            request.BotId);

        var result = await _sender.Send(command, cancellationToken);

        var response = new EmitSignalResponse(result.Value.Value);

        return ErrorOr<EmitSignalResponse>
            .Is(response)
            .Match(HandleError, Ok);
    }
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