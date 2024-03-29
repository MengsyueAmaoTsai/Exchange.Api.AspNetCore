using RichillCapital.Domain;
using RichillCapital.Domain.Common;
using RichillCapital.Domain.Trading;
using RichillCapital.Domain.Trading.Specifications;
using RichillCapital.SharedKernel.Monads;
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

        if (id.IsFailure)
        {
            return id.Error.ToErrorOr<IEnumerable<TradeDto>>();
        }

        var account = await _accountRepository.FirstOrDefaultAsync(
            new AccountByIdWithTradesSpecification(id.Value),
            cancellationToken);

        return account.IsNull ?
            DomainErrors.Accounts.NotFound(id.Value).ToErrorOr<IEnumerable<TradeDto>>() :
            account.Value.Trades
                .Select(TradeDto.From)
                .OrderByDescending(trade => trade.ExitTime)
                .ToList()
                .AsReadOnly()
                .ToErrorOr<IEnumerable<TradeDto>>();
    }
}