using RichillCapital.SharedKernel.Specifications;
using RichillCapital.SharedKernel.Specifications.Builders;

namespace RichillCapital.Domain.Trading.Specifications;

public sealed class AccountByIdWithPositionsSpecification :
    Specification<Account>
{
    public AccountByIdWithPositionsSpecification(AccountId accountId) =>
        Query
            .Where(account => account.Id == accountId)
            .Include(account => account.Positions);
}