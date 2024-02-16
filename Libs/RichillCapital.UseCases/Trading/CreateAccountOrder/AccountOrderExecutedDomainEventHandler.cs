using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Trading.Events;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.CreateAccountOrder;

internal sealed class AccountOrderExecutedDomainEventHandler(
    ILogger<AccountOrderExecutedDomainEventHandler> _logger) :
    IDomainEventHandler<AccountOrderExecutedDomainEvent>
{
    public async Task Handle(
        AccountOrderExecutedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "AccountOrderExecuted. Id={id}",
            domainEvent.OrderId.Value);
    }
}