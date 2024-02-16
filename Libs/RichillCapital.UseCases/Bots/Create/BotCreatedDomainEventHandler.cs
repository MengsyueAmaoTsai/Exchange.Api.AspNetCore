using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Bots;
using RichillCapital.Domain.Bots.Events;
using RichillCapital.Domain.Common;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Bots.Create;

internal sealed class BotCreatedDomainEventHandler(
    ILogger<BotCreatedDomainEventHandler> _logger,
    INotificationService _notificationService,
    IReadOnlyRepository<Bot> _botRepository,
    BotAccountsService _botAccountsService) :
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

        var bot = await _botRepository
            .GetByIdAsync(domainEvent.BotId, cancellationToken);

        if (bot.HasNoValue)
        {
            var error = DomainErrors.Bots.NotFound(domainEvent.BotId);
            throw new InvalidOperationException(error.Message);
        }

        var errorOr = await _botAccountsService
            .CreateSimulatedAccountAsync(
                domainEvent.BotId,
                cancellationToken);

        if (errorOr.IsError)
        {
            throw new InvalidOperationException(errorOr.Error.Message);
        }
    }
}