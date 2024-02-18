using System.Linq.Expressions;

using RichillCapital.SharedKernel.Monad;
using RichillCapital.SharedKernel.Specifications;

namespace RichillCapital.Domain.Common;

public interface IReadOnlyRepository<TEntity> :
    ISpecificationReadOnlyRepository<TEntity>
    where TEntity : class
{
    Task<Maybe<TEntity>> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default)
        where TId : notnull;

    Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default);

    Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    Task<int> CountAsync(CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    Task<Maybe<TEntity>> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    Task<int> CountAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
}