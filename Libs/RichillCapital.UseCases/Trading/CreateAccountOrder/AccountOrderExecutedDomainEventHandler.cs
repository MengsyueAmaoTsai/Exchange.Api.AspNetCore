using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Trading;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.CreateAccountOrder;

internal sealed class AccountOrderExecutedDomainEventHandler(
    ILogger<AccountOrderExecutedDomainEventHandler> _logger,
    INotificationService _notificationService) :
    IDomainEventHandler<AccountOrderExecutedDomainEvent>
{
    public async Task Handle(
        AccountOrderExecutedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Account order executed: {OrderId}",
            domainEvent.OrderId.Value);

        await _notificationService.SendLineNotificationAsync(
            $"Account order executed: {domainEvent.OrderId.Value}");
    }
}