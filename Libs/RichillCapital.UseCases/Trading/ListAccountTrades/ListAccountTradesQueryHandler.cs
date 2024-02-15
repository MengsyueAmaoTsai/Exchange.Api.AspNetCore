using RichillCapital.Domain;
using RichillCapital.Domain.Common;
using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.ListAccountTrades;

internal sealed class ListAccountTradesQueryHandler(
    IReadOnlyRepository<Account> _accountRepository) :
    IQueryHandler<ListAccountTradesQuery, ErrorOr<IEnumerable<TradeDto>>>
{
    public async Task<ErrorOr<IEnumerable<TradeDto>>> Handle(
        ListAccountTradesQuery query,
        CancellationToken cancellationToken)
    {
        var id = AccountId.From(query.AccountId);

        if (id.IsError)
        {
            return id.Error;
        }

        var account = await _accountRepository.FirstOrDefaultAsync(
            new AccountByIdWithTradesSpecification(id.Value),
            cancellationToken);

        return account.HasNoValue ?
            DomainErrors.Accounts.NotFound(id.Value) :
            account.Value.Trades
                .Select(trade => trade.ToDto())
                .ToList()
                .AsReadOnly();
    }
}