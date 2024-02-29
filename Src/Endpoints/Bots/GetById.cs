using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Bots;
using RichillCapital.UseCases.Bots.GetById;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Exchange.Api.Endpoints.Bots;

public sealed class GetById(ISender _sender) : AsyncEndpoint
    .WithRequest<GetBotByIdRequest>
    .WithActionResult<BotResponse>
{
    [HttpGet("/api/bots/{botId}")]
    [SwaggerOperation(
        Summary = "Gets a bot by its ID.",
        Description = "Gets a bot by its ID.",
        OperationId = "Bots.GetById",
        Tags = ["Bots"])]
    public override async Task<ActionResult<BotResponse>> HandleAsync(
        [FromRoute] GetBotByIdRequest request,
        CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    private GetBotByIdQuery ToQuery(GetBotByIdRequest request) =>
        new(request.BotId);

    private BotResponse ToResponse(BotDto bot) =>
        new(bot.Id, bot.Name, bot.Description, bot.Platform);
}

public sealed record class GetBotByIdRequest
{
    [FromRoute(Name = "botId")]
    public string BotId { get; init; } = string.Empty;
}