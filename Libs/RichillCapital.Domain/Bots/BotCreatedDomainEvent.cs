using RichillCapital.Domain.Common;

namespace RichillCapital.Domain.Bots;

public sealed record class BotCreatedDomainEvent(
    BotId BotId) :
    DomainEvent;