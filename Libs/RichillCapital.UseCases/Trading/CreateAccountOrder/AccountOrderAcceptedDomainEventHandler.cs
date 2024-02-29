using RichillCapital.Domain;
using RichillCapital.Domain.Common;
using RichillCapital.Domain.Trading;
using RichillCapital.Domain.Trading.Events;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.CreateAccountOrder;

internal sealed class AccountOrderAcceptedDomainEventHandler(
    INotificationService _notificationService,
    IReadOnlyRepository<Order> _orderRepository,
    OrderMatchingService _orderMatchingService) :
    IDomainEventHandler<AccountOrderAcceptedDomainEvent>
{
    public async Task Handle(
        AccountOrderAcceptedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(domainEvent.OrderId, cancellationToken);

        if (order.IsNull)
        {
            var error = DomainErrors.Orders.NotFound(domainEvent.OrderId);
            throw new InvalidOperationException(error.Message);
        }

        await _notificationService.SendLineNotificationAsync(
            $"AccountOrderAccepted. {domainEvent.OrderId.Value}");

        await _orderMatchingService.MatchOrderAsync(order.Value, cancellationToken);
    }
}