using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Bots.GetById;

public sealed record GetBotByIdQuery(
    string BotId) :
    IQuery<ErrorOr<BotDto>>;