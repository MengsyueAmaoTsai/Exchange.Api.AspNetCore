using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Trading;
using RichillCapital.Domain.Trading.Events;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.CreateAccountOrder;

internal sealed class AccountOrderRejectedDomainEventHandler(
    ILogger<AccountOrderRejectedDomainEventHandler> _logger,
    INotificationService _notificationService) :
    IDomainEventHandler<AccountOrderRejectedDomainEvent>
{
    public async Task Handle(
        AccountOrderRejectedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Account order rejected: {OrderId}",
            domainEvent.OrderId.Value);

        await _notificationService.SendLineNotificationAsync(
            $"Account order rejected: {domainEvent.OrderId.Value}");
    }
}