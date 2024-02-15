using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Bots;
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
            domainEvent.BotId);

        await _notificationService.SendLineNotificationAsync(
            $"Bot with id {domainEvent.BotId} created.");
    }
}