using RichillCapital.Domain;
using RichillCapital.Domain.Common;
using RichillCapital.Domain.Trading;
using RichillCapital.Domain.Trading.Specifications;
using RichillCapital.SharedKernel.Monads;
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

        if (id.IsFailure)
        {
            return id.Error.ToErrorOr<IEnumerable<ExecutionDto>>();
        }

        var account = await _accountRepository.FirstOrDefaultAsync(
            new AccountByIdWithExecutionsSpecification(id.Value),
            cancellationToken);

        return account.IsNull ?
            DomainErrors.Accounts.NotFound(id.Value).ToErrorOr<IEnumerable<ExecutionDto>>() :
            account.Value.Executions
                .Select(ExecutionDto.From)
                .OrderByDescending(execution => execution.Time)
                .ToList()
                .AsReadOnly()
                .ToErrorOr<IEnumerable<ExecutionDto>>();
    }
}