using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.ListAccountTrades;

public sealed record ListAccountTradesQuery(
    string AccountId) :
    IQuery<ErrorOr<IEnumerable<TradeDto>>>;