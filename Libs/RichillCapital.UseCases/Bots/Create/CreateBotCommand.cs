using RichillCapital.Domain.Bots;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Bots.Create;

public sealed record CreateBotCommand(
    string Id,
    string Name,
    string Description,
    string Platform) :
    ICommand<ErrorOr<BotId>>;