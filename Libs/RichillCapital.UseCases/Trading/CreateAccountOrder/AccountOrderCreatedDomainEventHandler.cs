using RichillCapital.Domain;
using RichillCapital.Domain.Common;
using RichillCapital.Domain.Trading;
using RichillCapital.Domain.Trading.Events;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.CreateAccountOrder;

internal sealed class AccountOrderCreatedDomainEventHandler(
    INotificationService _notificationService,
    IRepository<Order> _orderRepository,
    IUnitOfWork _unitOfWork) :
    IDomainEventHandler<AccountOrderCreatedDomainEvent>
{
    public async Task Handle(
        AccountOrderCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(domainEvent.OrderId, cancellationToken);

        if (order.IsNull)
        {
            var error = DomainErrors.Orders.NotFound(domainEvent.OrderId);
            throw new InvalidOperationException(error.Message);
        }

        await _notificationService.SendLineNotificationAsync(
            $"AccountOrderCreated. {domainEvent.OrderId.Value}");

        var result = order.Value.Accept();

        if (result.IsFailure)
        {
            throw new InvalidOperationException(result.Error.Message);
        }

        _orderRepository.Update(order.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}