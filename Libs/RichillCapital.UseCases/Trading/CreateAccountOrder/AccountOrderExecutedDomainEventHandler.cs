using Microsoft.Extensions.Logging;

using RichillCapital.Domain;
using RichillCapital.Domain.Common;
using RichillCapital.Domain.Trading;
using RichillCapital.Domain.Trading.Events;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.CreateAccountOrder;

internal sealed class AccountOrderExecutedDomainEventHandler(
    ILogger<AccountOrderExecutedDomainEventHandler> _logger,
    IRepository<Account> _accountRepository,
    IUnitOfWork _unitOfWork) :
    IDomainEventHandler<AccountOrderExecutedDomainEvent>
{
    public async Task Handle(
        AccountOrderExecutedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        var execution = domainEvent.Execution;
        _logger.LogInformation(
            "Order({OrderId}) {TradeType} {Symbol} {Quantity} @ {Price} executed.",
            execution.OrderId.Value,
            execution.TradeType.Name,
            execution.Symbol.Value,
            execution.Quantity,
            execution.Price);

        var account = await _accountRepository
            .GetByIdAsync(execution.AccountId, cancellationToken);

        if (account.HasNoValue)
        {
            var error = DomainErrors.Accounts.NotFound(execution.AccountId);
            throw new InvalidOperationException(error.Message);
        }

        var errorOr = Position.Open(
            execution.TradeType == TradeType.Buy ? Side.Long : Side.Short,
            execution.Symbol,
            execution.Quantity,
            execution.Price,
            execution.Commission,
            execution.Tax,
            account.Value.Id);

        if (errorOr.IsError)
        {
            throw new InvalidOperationException(errorOr.Errors.First().Message);
        }

        _accountRepository.Update(account.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}