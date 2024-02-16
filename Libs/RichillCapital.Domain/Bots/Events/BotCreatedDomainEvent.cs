using RichillCapital.Domain.Common;

namespace RichillCapital.Domain.Bots.Events;

public sealed record class BotCreatedDomainEvent(
    BotId BotId) :
    DomainEvent;