using System.ComponentModel.DataAnnotations;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.Exchange.Api.Extensions;
using RichillCapital.SharedKernel.Monad;
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
        CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();
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