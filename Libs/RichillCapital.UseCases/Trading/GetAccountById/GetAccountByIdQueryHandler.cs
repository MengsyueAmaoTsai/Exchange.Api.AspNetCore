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

        if (accountId.IsFailure)
        {
            return accountId.Error.ToErrorOr<AccountDto>();
        }

        var account = await _accountRepository.FirstOrDefaultAsync(
            new AccountByIdWithBalanceSpecification(accountId.Value),
            cancellationToken);

        return account.IsNull ?
            DomainErrors.Accounts.NotFound(accountId.Value).ToErrorOr<AccountDto>() :
            AccountDto.From(account.Value).ToErrorOr();
    }
}