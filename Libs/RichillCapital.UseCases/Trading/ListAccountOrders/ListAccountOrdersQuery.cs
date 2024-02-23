using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.ListAccountOrders;

public sealed record ListAccountOrdersQuery(
    string AccountId) :
    IQuery<ErrorOr<IEnumerable<OrderDto>>>;