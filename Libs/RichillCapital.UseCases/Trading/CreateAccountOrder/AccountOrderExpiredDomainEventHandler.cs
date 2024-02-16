using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Trading;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.CreateAccountOrder;

internal sealed class AccountOrderExpiredDomainEventHandler(
    ILogger<AccountOrderExpiredDomainEventHandler> _logger,
    INotificationService _notificationService) :
    IDomainEventHandler<AccountOrderExpiredDomainEvent>
{
    public async Task Handle(
        AccountOrderExpiredDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Account order expired: {OrderId}",
            domainEvent.OrderId.Value);

        await _notificationService.SendLineNotificationAsync(
            $"Account order expired: {domainEvent.OrderId.Value}");
    }
}