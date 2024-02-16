using RichillCapital.Domain;
using RichillCapital.Domain.Common;
using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.CreateAccountOrder;

internal sealed class CreateAccountOrderCommandHandler(
    IRepository<Account> _accountRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<CreateAccountOrderCommand, ErrorOr<OrderId>>
{
    public async Task<ErrorOr<OrderId>> Handle(
        CreateAccountOrderCommand command,
        CancellationToken cancellationToken)
    {
        var id = AccountId.From(command.AccountId);

        if (id.IsError)
        {
            return id.Error;
        }

        var account = await _accountRepository.GetByIdAsync(
            id.Value,
            cancellationToken);

        if (account.HasNoValue)
        {
            return DomainErrors.Accounts.NotFound(id.Value);
        }

        var tradeType = TradeType.FromName(command.TradeType);

        if (tradeType.HasNoValue)
        {
            return Error.Invalid("Invalid trade type");
        }

        var symbol = Symbol.From(command.Symbol);

        if (symbol.IsError)
        {
            return symbol.Error;
        }

        var orderType = OrderType.FromName(command.OrderType);

        if (orderType.HasNoValue)
        {
            return Error.Invalid("Invalid order type");
        }

        var timeInForce = TimeInForce.FromName(command.TimeInForce);

        if (timeInForce.HasNoValue)
        {
            return Error.Invalid("Invalid time in force");
        }

        var orderId = account.Value.CreateOrder(
            tradeType.Value,
            command.Quantity,
            symbol.Value,
            orderType.Value,
            timeInForce.Value);

        _accountRepository.Update(account.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return orderId.IsError ?
            orderId.Error :
            orderId.Value;
    }
}