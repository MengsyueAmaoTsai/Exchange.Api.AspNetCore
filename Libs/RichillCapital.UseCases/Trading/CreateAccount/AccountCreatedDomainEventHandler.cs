using RichillCapital.Domain.Trading.Events;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.CreateAccount;

internal sealed class AccountCreatedDomainEventHandler(
    INotificationService _notificationService) :
    IDomainEventHandler<AccountCreatedDomainEvent>
{
    public Task Handle(
        AccountCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken) =>
        _notificationService.SendLineNotificationAsync(
            $"AccountCreated. Id={domainEvent.AccountId.Value}");
}