using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Trading;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.CreateAccountOrder;

internal sealed class AccountOrderAcceptedDomainEventHandler(
    ILogger<AccountOrderAcceptedDomainEventHandler> _logger,
    INotificationService _notificationService) :
    IDomainEventHandler<AccountOrderAcceptedDomainEvent>
{
    public async Task Handle(
        AccountOrderAcceptedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Account order accepted: {OrderId}",
            domainEvent.OrderId.Value);

        await _notificationService.SendLineNotificationAsync(
            $"Account order accepted: {domainEvent.OrderId.Value}");
    }
}