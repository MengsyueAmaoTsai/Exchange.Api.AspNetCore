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

        var currency = Currency.FromName(command.Currency);

        if (currency.HasNoValue)
        {
            return Error.Invalid("Currency is invalid");
        }

        var account = Account.Create(name.Value, currency.Value);

        _accountRepository.Add(account);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return account.Id;
    }
}