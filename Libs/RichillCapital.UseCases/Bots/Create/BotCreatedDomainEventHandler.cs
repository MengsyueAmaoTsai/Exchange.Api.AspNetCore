using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Bots.Events;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Bots.Create;

internal sealed class BotCreatedDomainEventHandler(
    ILogger<BotCreatedDomainEventHandler> _logger,
    INotificationService _notificationService) :
    IDomainEventHandler<BotCreatedDomainEvent>
{
    public async Task Handle(
        BotCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Bot with id {BotId} created.",
            domainEvent.BotId.Value);

        await _notificationService.SendLineNotificationAsync(
            $"Bot with id {domainEvent.BotId.Value} created.");

        // TODO: Create simulated account and mock account with initial deposit for bot.
        // Initial deposit based on default value of currency or from back test result drawdown.
    }
}