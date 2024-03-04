using RichillCapital.Domain;
using RichillCapital.Domain.Common;
using RichillCapital.Domain.Shared;
using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
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

        if (id.IsFailure)
        {
            return id.Error.ToErrorOr<OrderId>();
        }

        var account = await _accountRepository.GetByIdAsync(
            id.Value,
            cancellationToken);

        if (account.IsNull)
        {
            return DomainErrors.Accounts
                .NotFound(id.Value)
                .ToErrorOr<OrderId>();
        }

        var tradeType = TradeType.FromName(command.TradeType);

        if (tradeType.IsNull)
        {
            return Error
                .Invalid("Invalid trade type")
                .ToErrorOr<OrderId>();
        }

        var symbol = Symbol.From(command.Symbol);

        if (symbol.IsFailure)
        {
            return symbol.Error
                .ToErrorOr<OrderId>();
        }

        var orderType = OrderType.FromName(command.OrderType);

        if (orderType.IsNull)
        {
            return Error
                .Invalid("Invalid order type")
                .ToErrorOr<OrderId>();
        }

        var timeInForce = TimeInForce.FromName(command.TimeInForce);

        if (timeInForce.IsNull)
        {
            return Error
                .Invalid("Invalid time in force")
                .ToErrorOr<OrderId>();
        }

        var orderId = account.Value.CreateOrder(
            tradeType.Value,
            command.Quantity,
            symbol.Value,
            orderType.Value,
            timeInForce.Value);

        _accountRepository.Update(account.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return orderId.HasError ?
            orderId.Errors.ToErrorOr<OrderId>() :
            orderId.Value.ToErrorOr();
    }
}