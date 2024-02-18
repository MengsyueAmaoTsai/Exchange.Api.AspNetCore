using RichillCapital.Domain.Common;

namespace RichillCapital.Domain.Trading.Events;

public sealed record class AccountPositionClosedDomainEvent(
    PositionId PositionId,
    AccountId AccountId) :
    DomainEvent;