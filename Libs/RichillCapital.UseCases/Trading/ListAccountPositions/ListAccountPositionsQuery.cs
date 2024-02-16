using RichillCapital.SharedKernel.Monad;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.ListAccountPositions;

public sealed record ListAccountPositionsQuery(
    string AccountId) :
    IQuery<ErrorOr<IEnumerable<PositionDto>>>;