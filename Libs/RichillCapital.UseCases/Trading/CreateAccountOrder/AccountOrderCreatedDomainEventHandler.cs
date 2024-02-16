using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Trading;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.CreateAccountOrder;

internal sealed class AccountOrderCreatedDomainEventHandler(
    ILogger<AccountOrderCreatedDomainEventHandler> _logger,
    INotificationService _notificationService) :
    IDomainEventHandler<AccountOrderCreatedDomainEvent>
{
    public async Task Handle(
        AccountOrderCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Account order created: {OrderId}",
            domainEvent.OrderId);

        await _notificationService.SendLineNotificationAsync(
            $"Account order created: {domainEvent.OrderId.Value}");
    }
}