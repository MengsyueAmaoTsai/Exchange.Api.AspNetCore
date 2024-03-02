using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Bots;
using RichillCapital.Domain.Bots.Events;
using RichillCapital.Domain.Common;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Bots.Create;

internal sealed class BotCreatedDomainEventHandler(
    ILogger<BotCreatedDomainEventHandler> _logger,
    IReadOnlyRepository<Bot> _botRepository,
    BotAccountsService _botAccountsService) :
    IDomainEventHandler<BotCreatedDomainEvent>
{
    public async Task Handle(
        BotCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        var bot = await _botRepository
            .GetByIdAsync(domainEvent.BotId, cancellationToken);

        if (bot.IsNull)
        {
            var error = DomainErrors.Bots.NotFound(domainEvent.BotId);
            throw new InvalidOperationException(error.Message);
        }

        _logger.LogInformation(
            "BotCreated. Id={id}, Name={name}, Description={description}, Platform={platform}",
            bot.Value.Id.Value,
            bot.Value.Name.Value,
            bot.Value.Description.Value,
            bot.Value.Platform.Name);

        var errorOr = await _botAccountsService
            .CreateSimulatedAccountAsync(
                domainEvent.BotId,
                cancellationToken);

        if (errorOr.HasError)
        {
            throw new InvalidOperationException(errorOr.Errors.First().Message);
        }
    }
}