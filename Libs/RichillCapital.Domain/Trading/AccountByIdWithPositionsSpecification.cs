using RichillCapital.SharedKernel.Specifications;

namespace RichillCapital.Domain.Trading;

public sealed class AccountByIdWithPositionsSpecification :
    Specification<Account>
{
    public AccountByIdWithPositionsSpecification(AccountId accountId) =>
        Query
            .Where(account => account.Id == accountId)
            .Include(account => account.Positions);
}