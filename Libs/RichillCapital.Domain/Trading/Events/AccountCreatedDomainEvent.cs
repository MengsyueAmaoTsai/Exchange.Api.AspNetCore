using RichillCapital.Domain.Common;

namespace RichillCapital.Domain.Trading.Events;

public sealed record class AccountCreatedDomainEvent(
    AccountId AccountId) :
    DomainEvent;