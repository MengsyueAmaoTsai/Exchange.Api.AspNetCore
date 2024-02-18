using RichillCapital.SharedKernel.Specifications;
using RichillCapital.SharedKernel.Specifications.Builders;

namespace RichillCapital.Domain.Trading.Specifications;

public sealed class AccountByIdWithOrdersSpecification :
    Specification<Account>
{
    public AccountByIdWithOrdersSpecification(AccountId accountId) =>
        Query
            .Where(account => account.Id == accountId)
            .Include(account => account.Orders);
}