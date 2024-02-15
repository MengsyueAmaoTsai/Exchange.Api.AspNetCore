using RichillCapital.Domain.Common;

namespace RichillCapital.Domain.Trading;

public sealed record class AccountCreatedDomainEvent(
    AccountId AccountId) :
    DomainEvent;