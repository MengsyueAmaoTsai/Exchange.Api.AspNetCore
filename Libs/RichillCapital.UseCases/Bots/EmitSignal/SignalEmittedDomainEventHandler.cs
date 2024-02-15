using Microsoft.Extensions.Logging;

using RichillCapital.Domain.Bots;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Bots.EmitSignal;

internal sealed class SignalEmittedDomainEventHandler(
    ILogger<SignalEmittedDomainEventHandler> _logger) :
    IDomainEventHandler<SignalEmittedDomainEvent>
{
    public Task Handle(
        SignalEmittedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Signal emitted for bot with id {BotId}.",
            domainEvent.BotId);

        return Task.CompletedTask;
    }
}