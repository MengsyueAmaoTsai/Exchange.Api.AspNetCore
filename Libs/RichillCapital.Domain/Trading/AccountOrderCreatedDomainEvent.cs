using RichillCapital.Domain.Common;

namespace RichillCapital.Domain.Trading;

public sealed record class AccountOrderCreatedDomainEvent(
    OrderId OrderId) :
    DomainEvent;