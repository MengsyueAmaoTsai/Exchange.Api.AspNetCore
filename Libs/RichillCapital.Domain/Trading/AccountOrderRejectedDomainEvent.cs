using RichillCapital.Domain.Common;

namespace RichillCapital.Domain.Trading;

public sealed record class AccountOrderRejectedDomainEvent(
    OrderId OrderId) :
    DomainEvent;