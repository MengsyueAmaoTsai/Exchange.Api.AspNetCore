using MediatR;

namespace RichillCapital.UseCases.Common;

public interface IQueryHandler<TQuery, TResult> :
    IRequestHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    new Task<TResult> Handle(TQuery query, CancellationToken cancellationToken);
}