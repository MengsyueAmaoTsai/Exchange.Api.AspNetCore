using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.GetAccountPerformance;

public sealed record GetAccountPerformanceQuery(
    string AccountId) :
    IQuery<ErrorOr<AccountPerformanceDto>>;