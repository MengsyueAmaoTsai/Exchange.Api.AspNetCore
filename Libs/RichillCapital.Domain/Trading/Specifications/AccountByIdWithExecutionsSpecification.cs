using RichillCapital.SharedKernel.Specifications;

namespace RichillCapital.Domain.Trading.Specifications;

public sealed class AccountByIdWithExecutionsSpecification :
    Specification<Account>
{
    public AccountByIdWithExecutionsSpecification(AccountId accountId) =>
        Query
            .Where(account => account.Id == accountId)
            .Include(account => account.Executions);
}