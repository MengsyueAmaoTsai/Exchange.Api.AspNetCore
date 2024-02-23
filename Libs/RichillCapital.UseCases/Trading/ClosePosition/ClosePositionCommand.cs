using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.ClosePosition;

public sealed record ClosePositionCommand(string PositionId) :
    ICommand<ErrorOr<PositionId>>;