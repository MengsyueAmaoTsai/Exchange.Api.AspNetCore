using RichillCapital.SharedKernel.Specifications;

namespace RichillCapital.Domain.Trading.Specifications;

public sealed class AccountByIdWithBalanceSpecification :
Specification<Account>
{
    public AccountByIdWithBalanceSpecification(AccountId accountId) =>
        Query
            .Where(account => account.Id == accountId)
            .Include(account => account.Balances);
}