using RichillCapital.Domain;
using RichillCapital.Domain.Common;
using RichillCapital.Domain.Trading;
using RichillCapital.Domain.Trading.Specifications;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.GetAccountPerformance;

internal sealed class GetAccountPerformanceQueryHandler(
    IReadOnlyRepository<Account> _accountRepository) :
    IQueryHandler<GetAccountPerformanceQuery, ErrorOr<AccountPerformanceDto>>
{
    public async Task<ErrorOr<AccountPerformanceDto>> Handle(
        GetAccountPerformanceQuery query,
        CancellationToken cancellationToken)
    {
        var id = AccountId.From(query.AccountId);

        if (id.IsFailure)
        {
            return id.Error.ToErrorOr<AccountPerformanceDto>();
        }

        var account = await _accountRepository.FirstOrDefaultAsync(
            new AccountByIdWithTradesSpecification(id.Value),
            cancellationToken);

        return account.IsNull ?
            DomainErrors.Accounts.NotFound(id.Value).ToErrorOr<AccountPerformanceDto>() :
            AccountPerformanceDto
                .From(AccountPerformance.GenerateFromClosedTrades(account.Value.Trades))
                .ToErrorOr();
    }
}
