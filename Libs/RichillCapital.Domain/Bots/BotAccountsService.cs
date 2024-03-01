using RichillCapital.Domain.Common;
using RichillCapital.Domain.Shared;
using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel.Monads;

namespace RichillCapital.Domain.Bots;

public sealed class BotAccountsService(
    IRepository<Account> _accountRepository,
    IUnitOfWork _unitOfWork)
{
    public async Task<ErrorOr<AccountId>> CreateSimulatedAccountAsync(
        BotId botId,
        CancellationToken cancellationToken = default)
    {
        var prefix = "SIM";
        var accountName = AccountName.From($"{prefix}-{botId.Value}");

        if (accountName.IsFailure)
        {
            return accountName.Error.ToErrorOr<AccountId>();
        }

        var account = Account.Create(
            accountName.Value,
            PositionMode.Hedging,
            Currency.TWD);

        if (account.IsFailure)
        {
            return account.Error.ToErrorOr<AccountId>();
        }

        _accountRepository.Add(account.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ErrorOr<AccountId>
            .With(account.Value.Id);
    }
}