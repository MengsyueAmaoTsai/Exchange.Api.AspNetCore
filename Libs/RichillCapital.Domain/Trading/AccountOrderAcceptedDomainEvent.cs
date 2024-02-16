using RichillCapital.Domain.Common;

namespace RichillCapital.Domain.Trading;

public sealed record class AccountOrderAcceptedDomainEvent(
    OrderId OrderId) :
    DomainEvent;