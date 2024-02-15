using RichillCapital.Domain.Common;

namespace RichillCapital.Domain.Bots;

public sealed record class BotSignalEmittedDomainEvent(
    BotId BotId) :
    DomainEvent;