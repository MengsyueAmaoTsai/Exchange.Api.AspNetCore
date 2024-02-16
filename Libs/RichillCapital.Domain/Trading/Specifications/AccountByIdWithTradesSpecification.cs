using RichillCapital.SharedKernel.Specifications;

namespace RichillCapital.Domain.Trading.Specifications;

public sealed class AccountByIdWithTradesSpecification :
    Specification<Account>
{
    public AccountByIdWithTradesSpecification(AccountId accountId) =>
        Query
            .Where(account => account.Id == accountId)
            .Include(account => account.Trades);
}