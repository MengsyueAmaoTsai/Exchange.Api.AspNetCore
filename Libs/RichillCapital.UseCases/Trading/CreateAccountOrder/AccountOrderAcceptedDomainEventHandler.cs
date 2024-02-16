using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Common;
using RichillCapital.Domain.Trading;
using RichillCapital.Domain.Trading.Events;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.CreateAccountOrder;

internal sealed class AccountOrderAcceptedDomainEventHandler(
    ILogger<AccountOrderAcceptedDomainEventHandler> _logger,
    INotificationService _notificationService,
    IReadOnlyRepository<Order> _orderRepository,
    OrderMatchingService _orderMatchingService) :
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

        var order = await _orderRepository.GetByIdAsync(domainEvent.OrderId, cancellationToken);

        if (order.HasNoValue)
        {
            var error = DomainErrors.Orders.NotFound(domainEvent.OrderId);
            throw new InvalidOperationException(error.Message);
        }

        await _orderMatchingService.MatchOrderAsync(order.Value, cancellationToken);
    }
}