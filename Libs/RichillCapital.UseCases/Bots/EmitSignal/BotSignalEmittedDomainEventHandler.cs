using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Bots.Events;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Bots.EmitSignal;

internal sealed class BotSignalEmittedDomainEventHandler(
    ILogger<BotSignalEmittedDomainEvent> _logger,
    INotificationService _notificationService) :
    IDomainEventHandler<BotSignalEmittedDomainEvent>
{
    public async Task Handle(
        BotSignalEmittedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Signal emitted for bot with id {BotId}.",
            domainEvent.BotId);

        await _notificationService.SendLineNotificationAsync(
            $"Signal emitted for bot with id {domainEvent.BotId.Value}.");
    }
}