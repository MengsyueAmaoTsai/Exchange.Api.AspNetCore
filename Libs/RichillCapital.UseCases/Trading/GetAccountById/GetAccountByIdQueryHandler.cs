using RichillCapital.Domain;
using RichillCapital.Domain.Common;
using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.GetAccountById;

internal sealed class GetAccountByIdQueryHandler(
    IReadOnlyRepository<Account> _accountRepository) :
    IQueryHandler<GetAccountByIdQuery, ErrorOr<AccountDto>>
{
    public async Task<ErrorOr<AccountDto>> Handle(
        GetAccountByIdQuery query,
        CancellationToken cancellationToken)
    {
        var accountId = AccountId.From(query.AccountId);

        if (accountId.IsError)
        {
            return accountId.Error;
        }

        var account = await _accountRepository.GetByIdAsync(
            accountId.Value,
            cancellationToken);

        return account.HasNoValue ?
            DomainErrors.Accounts.NotFound(accountId.Value) :
            account.Value.ToDto();
    }
}