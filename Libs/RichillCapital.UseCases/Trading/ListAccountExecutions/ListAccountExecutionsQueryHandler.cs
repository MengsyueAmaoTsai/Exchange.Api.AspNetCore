using RichillCapital.Domain;
using RichillCapital.Domain.Common;
using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.ListAccountExecutions;

internal sealed class ListAccountExecutionsQueryHandler(
    IReadOnlyRepository<Account> _accountRepository) :
    IQueryHandler<ListAccountExecutionsQuery, ErrorOr<IEnumerable<ExecutionDto>>>
{
    public async Task<ErrorOr<IEnumerable<ExecutionDto>>> Handle(
        ListAccountExecutionsQuery query,
        CancellationToken cancellationToken)
    {
        var id = AccountId.From(query.AccountId);

        if (id.IsError)
        {
            return id.Error;
        }

        var account = await _accountRepository.FirstOrDefaultAsync(
            new AccountByIdWithExecutionsSpecification(id.Value),
            cancellationToken);

        return account.HasNoValue ?
            DomainErrors.Accounts.NotFound(id.Value) :
            account.Value.Executions
                .Select(execution => execution.ToDto())
                .ToList()
                .AsReadOnly();
    }
}