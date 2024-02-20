using RichillCapital.Domain;
using RichillCapital.Domain.Common;
using RichillCapital.Domain.Trading;
using RichillCapital.Domain.Trading.Specifications;
using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.ListAccountOrders;

internal sealed class ListAccountOrdersQueryHandler(
    IReadOnlyRepository<Account> _accountRepository) :
    IQueryHandler<ListAccountOrdersQuery, ErrorOr<IEnumerable<OrderDto>>>
{
    public async Task<ErrorOr<IEnumerable<OrderDto>>> Handle(
        ListAccountOrdersQuery query,
        CancellationToken cancellationToken)
    {
        var id = AccountId.From(query.AccountId);

        if (id.IsError)
        {
            return id.Error;
        }

        var account = await _accountRepository.FirstOrDefaultAsync(
            new AccountByIdWithOrdersSpecification(id.Value),
            cancellationToken);

        return account.HasNoValue ?
            DomainErrors.Accounts.NotFound(id.Value) :
            account.Value.Orders
                .Select(OrderDto.From)
                .OrderByDescending(order => order.Time)
                .ToList()
                .AsReadOnly();
    }
}