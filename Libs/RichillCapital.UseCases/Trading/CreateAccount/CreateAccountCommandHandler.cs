using RichillCapital.Domain.Common;
using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monad;
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
            return name.Error;
        }

        if (await _accountRepository.AnyAsync(
            account => account.Name == name.Value,
            cancellationToken))
        {
            return Error.Conflict("Account with the same name already exists");
        }

        var currency = Currency.FromName(command.Currency);

        if (currency.HasNoValue)
        {
            return Error.Invalid("Currency is invalid");
        }

        var account = Account.Create(name.Value, currency.Value);

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