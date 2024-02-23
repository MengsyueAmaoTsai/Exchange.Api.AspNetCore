using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.ListAccountExecutions;

public sealed record ListAccountExecutionsQuery(
    string AccountId) :
    IQuery<ErrorOr<IEnumerable<ExecutionDto>>>;