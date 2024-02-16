using RichillCapital.Domain.Common;

namespace RichillCapital.Domain.Trading.Events;

public sealed record class AccountOrderAcceptedDomainEvent(
    OrderId OrderId) :
    DomainEvent;