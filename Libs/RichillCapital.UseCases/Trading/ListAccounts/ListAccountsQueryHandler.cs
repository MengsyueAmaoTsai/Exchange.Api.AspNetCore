using MediatR;

using RichillCapital.Domain.Common;
using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.ListAccounts;

internal sealed class ListAccountsQueryHandler(
    IReadOnlyRepository<Account> _accountRepository) :
    IQueryHandler<ListAccountsQuery, Result<IEnumerable<AccountDto>>>
{
    public async Task<Result<IEnumerable<AccountDto>>> Handle(
        ListAccountsQuery query,
        CancellationToken cancellationToken) =>
        (await _accountRepository.ListAsync(cancellationToken))
            .Select(account => account.ToDto())
            .ToList()
            .AsReadOnly();
}