using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Trading.Events;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.CreateAccount;

internal sealed class AccountCreatedDomainEventHandler(
    ILogger<AccountCreatedDomainEventHandler> _logger,
    INotificationService _notificationService) :
    IDomainEventHandler<AccountCreatedDomainEvent>
{
    public Task Handle(
        AccountCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Account created: {AccountId}",
            domainEvent.AccountId.Value);

        return _notificationService.SendLineNotificationAsync(
            $"Account created: {domainEvent.AccountId.Value}");
    }
}