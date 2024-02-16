using RichillCapital.Domain.Common;

namespace RichillCapital.Domain.Trading.Events;

public sealed record class AccountOrderCreatedDomainEvent(
    OrderId OrderId) :
    DomainEvent;