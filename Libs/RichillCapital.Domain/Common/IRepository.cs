using RichillCapital.SharedKernel.Specifications;

namespace RichillCapital.Domain.Common;

public interface IRepository<TEntity> :
    IReadOnlyRepository<TEntity>,
    ISpecificationRepository<TEntity>
    where TEntity : class
{
    void Add(TEntity entity);

    void AddRange(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    void UpdateRange(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);

    void RemoveRange(IEnumerable<TEntity> entities);
}