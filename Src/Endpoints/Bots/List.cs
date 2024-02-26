using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RichillCapital.Exchange.Api.Common;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Bots;
using RichillCapital.UseCases.Bots.List;

using Swashbuckle.AspNetCore.Annotations;

namespace RichillCapital.Exchange.Api.Endpoints.Bots;

public sealed class List(
    ISender _sender,
    IMapper _mapper) : AsyncEndpoint
    .WithRequest<ListBotsRequest>
    .WithActionResult<IEnumerable<BotResponse>>
{
    [HttpGet("/api/bots")]
    [SwaggerOperation(
        Summary = "Lists all bots.",
        Description = "Lists all bots.",
        OperationId = "Bots.List",
        Tags = ["Bots"])]
    public override async Task<ActionResult<IEnumerable<BotResponse>>> HandleAsync(
        [FromQuery] ListBotsRequest request,
        CancellationToken cancellationToken = default) =>
        await Result<ListBotsRequest>.Success(request)
            .Map(ToQuery)
            .Then(query => _sender.Send(query, cancellationToken))
            .Map(ToResponse)
            .Match(Ok, HandleError);

    private ListBotsQuery ToQuery(ListBotsRequest request) =>
        _mapper.Map<ListBotsQuery>(request);

    private IEnumerable<BotResponse> ToResponse(IEnumerable<BotDto> bots) =>
        _mapper.Map<IEnumerable<BotResponse>>(bots);
}

public sealed record class ListBotsRequest
{
}
