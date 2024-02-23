using RichillCapital.Domain;
using RichillCapital.Domain.Common;
using RichillCapital.Domain.Trading;
using RichillCapital.Domain.Trading.Specifications;
using RichillCapital.SharedKernel.Monads;
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
            return accountId.Errors.ToList();
        }

        var account = await _accountRepository.FirstOrDefaultAsync(
            new AccountByIdWithBalanceSpecification(accountId.Value),
            cancellationToken);

        return account.HasNoValue ?
            DomainErrors.Accounts.NotFound(accountId.Value) :
            AccountDto.From(account.Value);
    }
}