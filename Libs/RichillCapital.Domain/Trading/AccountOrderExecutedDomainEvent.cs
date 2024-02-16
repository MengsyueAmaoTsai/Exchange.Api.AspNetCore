using RichillCapital.Domain.Common;

namespace RichillCapital.Domain.Trading;

public sealed record class AccountOrderExecutedDomainEvent(
    OrderId OrderId) :
    DomainEvent;