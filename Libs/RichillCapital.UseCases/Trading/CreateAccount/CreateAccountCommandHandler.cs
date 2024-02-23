using RichillCapital.Domain;
using RichillCapital.Domain.Common;
using RichillCapital.Domain.Shared;
using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.CreateAccount;

internal sealed class CreateAccountCommandHandler(
    IRepository<Account> _accountRepository,
    IUnitOfWork _unitOfWork) :
    ICommandHandler<CreateAccountCommand, ErrorOr<AccountId>>
{
    public async Task<ErrorOr<AccountId>> Handle(
        CreateAccountCommand command,
        CancellationToken cancellationToken)
    {
        var name = AccountName.From(command.Name);

        if (name.IsError)
        {
            return name.Errors.ToList();
        }

        if (await _accountRepository.AnyAsync(
            account => account.Name == name.Value,
            cancellationToken))
        {
            return DomainErrors.Accounts.AlreadyExists(name.Value); ;
        }

        var positionMode = PositionMode.FromName(command.PositionMode);

        if (positionMode.HasNoValue)
        {
            return Error.Invalid("Position mode is invalid");
        }

        var currency = Currency.FromName(command.Currency);

        if (currency.HasNoValue)
        {
            return Error.Invalid("Currency is invalid");
        }

        var account = Account.Create(
            name.Value,
            positionMode.Value,
            currency.Value);

        if (account.IsFailure)
        {
            return account.Error;
        }

        account.Value.WithBalance(currency.Value, command.InitialDeposit);

        _accountRepository.Add(account.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return account.Value.Id;
    }
}