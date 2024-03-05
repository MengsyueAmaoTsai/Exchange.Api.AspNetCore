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

        var mockAccount = Account.Create(
            AccountId.NewMockAccountId(),
            accountName.Value,
            PositionMode.Hedging,
            TradingEnvironment.Mock,
            Currency.TWD);

        if (mockAccount.IsFailure)
        {
            return mockAccount.Error.ToErrorOr<AccountId>();
        }

        var simulatedAccount = Account.Create(
            AccountId.NewSimulatedAccountId(),
            accountName.Value,
            PositionMode.Hedging,
            TradingEnvironment.Simulated,
            Currency.TWD);

        _accountRepository.AddRange([mockAccount.Value, simulatedAccount.Value]);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ErrorOr<AccountId>
            .With(mockAccount.Value.Id);
    }
}