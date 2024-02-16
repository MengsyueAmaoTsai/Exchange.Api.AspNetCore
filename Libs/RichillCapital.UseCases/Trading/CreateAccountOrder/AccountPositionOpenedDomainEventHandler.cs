using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Trading.Events;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.CreateAccountOrder;

internal sealed class AccountPositionOpenedDomainEventHandler(
    ILogger<AccountPositionOpenedDomainEventHandler> _logger) :
    IDomainEventHandler<AccountPositionOpenedDomainEvent>
{
    public Task Handle(
        AccountPositionOpenedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Account {AccountId} position opened: {PositionId}",
            domainEvent.AccountId.Value,
            domainEvent.PositionId.Value);

        return Task.CompletedTask;
    }
}