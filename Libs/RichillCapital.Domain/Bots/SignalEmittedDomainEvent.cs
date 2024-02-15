using RichillCapital.Domain.Common;

namespace RichillCapital.Domain.Bots;

public sealed record class SignalEmittedDomainEvent(BotId BotId) :
    DomainEvent;