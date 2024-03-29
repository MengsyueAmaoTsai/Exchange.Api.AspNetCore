using RichillCapital.Domain;
using RichillCapital.Domain.Common;
using RichillCapital.Domain.Trading;
using RichillCapital.SharedKernel.Monads;
using RichillCapital.UseCases.Common;

namespace RichillCapital.UseCases.Trading.ClosePosition;

internal sealed class ClosePositionCommandHandler(
    IRepository<Position> _positionRepository,
    IUnitOfWork _unitOfWork)
    : ICommandHandler<ClosePositionCommand, ErrorOr<PositionId>>
{
    public async Task<ErrorOr<PositionId>> Handle(
        ClosePositionCommand command,
        CancellationToken cancellationToken)
    {
        var positionId = PositionId.From(command.PositionId);

        if (positionId.HasError)
        {
            return positionId.Errors.ToErrorOr<PositionId>();
        }

        var position = await _positionRepository
            .GetByIdAsync(positionId.Value, cancellationToken);

        if (position.IsNull)
        {
            return DomainErrors.Positions
                .NotFound(positionId.Value)
                .ToErrorOr<PositionId>();
        }

        position.Value.Close();

        _positionRepository.Update(position.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return position.Value.Id.ToErrorOr();
    }
}