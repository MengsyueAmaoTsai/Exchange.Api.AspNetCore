using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Bots;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Bots.EmitSignal;

internal sealed class SignalEmittedDomainEventHandler(
    ILogger<SignalEmittedDomainEventHandler> _logger,
    INotificationService _notificationService) :
    IDomainEventHandler<SignalEmittedDomainEvent>
{
    public async Task Handle(
        SignalEmittedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Signal emitted for bot with id {BotId}.",
            domainEvent.BotId);

        await _notificationService.SendLineNotificationAsync(
            $"Signal emitted for bot with id {domainEvent.BotId}.");
    }
}