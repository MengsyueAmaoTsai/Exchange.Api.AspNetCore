using RichillCapital.Domain;
using RichillCapital.Domain.Common;
using RichillCapital.Domain.Trading;
using RichillCapital.Domain.Trading.Specifications;
using RichillCapital.SharedKernel;
using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.ListAccountPositions;

internal sealed class ListAccountPositionsQueryHandler(
    IReadOnlyRepository<Account> _accountRepository) :
    IQueryHandler<ListAccountPositionsQuery, ErrorOr<IEnumerable<PositionDto>>>
{
    public async Task<ErrorOr<IEnumerable<PositionDto>>> Handle(
        ListAccountPositionsQuery query,
        CancellationToken cancellationToken)
    {
        var id = AccountId.From(query.AccountId);

        if (id.IsError)
        {
            return id.Error;
        }

        var account = await _accountRepository.FirstOrDefaultAsync(
            new AccountByIdWithPositionsSpecification(id.Value),
            cancellationToken);

        return account.HasNoValue ?
            DomainErrors.Accounts.NotFound(id.Value) :
            account.Value.Positions
                .Select(position => position.ToDto())
                .ToList()
                .AsReadOnly();
    }
}