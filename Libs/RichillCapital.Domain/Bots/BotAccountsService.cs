using RichillCapital.Domain.Common;
using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel.Monad;

namespace RichillCapital.Domain.Bots;

public sealed class BotAccountsService(
    IRepository<Account> _accountRepository,
    IUnitOfWork _unitOfWork)
{
    public async Task<ErrorOr> CreateSimulatedAccountAsync(
        BotId botId,
        CancellationToken cancellationToken = default)
    {
        var prefix = "SIM";
        var accountName = AccountName.From($"{prefix}-{botId.Value}");

        if (accountName.IsError)
        {
            return accountName.Error;
        }

        var account = Account.Create(
            accountName.Value,
            PositionMode.Hedging,
            Currency.TWD);

        if (account.IsFailure)
        {
            return account.Error;
        }

        _accountRepository.Add(account.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ErrorOr.NoError;
    }
}