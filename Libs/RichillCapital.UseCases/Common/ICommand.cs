using MediatR;

namespace RichillCapital.UseCases.Common;

public interface ICommand<TResult> :
    IRequest<TResult>
{
}