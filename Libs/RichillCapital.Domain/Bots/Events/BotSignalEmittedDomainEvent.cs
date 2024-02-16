using RichillCapital.Domain.Common;

namespace RichillCapital.Domain.Bots.Events;

public sealed record class BotSignalEmittedDomainEvent(
    BotId BotId) :
    DomainEvent;