using RichillCapital.Domain.Common;

namespace RichillCapital.Domain.Trading;

public sealed record class AccountOrderExpiredDomainEvent(
    OrderId OrderId) :
    DomainEvent;