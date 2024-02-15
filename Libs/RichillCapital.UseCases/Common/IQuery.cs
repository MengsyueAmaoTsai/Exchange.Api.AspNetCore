using MediatR;

namespace RichillCapital.UseCases.Common;

public interface IQuery<TResult> :
    IRequest<TResult>
{
}