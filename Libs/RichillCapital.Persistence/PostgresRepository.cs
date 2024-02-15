using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using RichillCapital.Domain.Common;
using RichillCapital.SharedKernel.Monad;
using RichillCapital.SharedKernel.Specifications;

namespace RichillCapital.Persistence;

public sealed class PostgresRepository<TEntity>
    : IRepository<TEntity>
    where TEntity : class
{
    private readonly PostgreSqlOptionsDbContext _dbContext;

    public PostgresRepository(PostgreSqlOptionsDbContext dbContext) =>
        _dbContext = dbContext;

    public void Add(TEntity entity) => _dbContext.Set<TEntity>().Add(entity);

    public void AddRange(IEnumerable<TEntity> entities) =>
        _dbContext.Set<TEntity>().AddRange(entities);

    public Task<bool> AnyAsync(CancellationToken cancellationToken = default) =>
        _dbContext.Set<TEntity>().AnyAsync(cancellationToken);

    public Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken) =>
        _dbContext.Set<TEntity>().AnyAsync(expression, cancellationToken);

    public async Task<bool> AnyAsync(
        Specification<TEntity> specification,
        CancellationToken cancellationToken = default) =>
        await ApplySpecification(specification).AnyAsync(cancellationToken);

    public Task<int> CountAsync(CancellationToken cancellationToken = default) =>
        _dbContext.Set<TEntity>().CountAsync(cancellationToken);

    public Task<int> CountAsync(
        Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken) =>
        _dbContext.Set<TEntity>().CountAsync(expression, cancellationToken);

    public async Task<int> CountAsync(
        Specification<TEntity> specification,
        CancellationToken cancellationToken = default) =>
        await ApplySpecification(specification).CountAsync(cancellationToken);

    public async Task<Maybe<TEntity>> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken) =>
        await _dbContext.Set<TEntity>().FirstOrDefaultAsync(expression, cancellationToken);

    public async Task<Maybe<TResult>> FirstOrDefaultAsync<TResult>(
        Specification<TEntity, TResult> specification,
        CancellationToken cancellationToken = default) =>
        await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);

    public async Task<Maybe<TEntity>> FirstOrDefaultAsync(
        Specification<TEntity> specification,
        CancellationToken cancellationToken = default) =>
        await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);

    public async Task<Maybe<TEntity>> GetByIdAsync<TEntityIdentifier>(
        TEntityIdentifier id,
        CancellationToken cancellationToken = default)
        where TEntityIdentifier : notnull =>
        await _dbContext.Set<TEntity>().FindAsync([id], cancellationToken);

    public Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default) =>
        _dbContext.Set<TEntity>().ToListAsync(cancellationToken);

    public async Task<List<TEntity>> ListAsync(
        Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken) =>
        await _dbContext.Set<TEntity>()
            .Where(expression).ToListAsync(cancellationToken);

    public async Task<List<TResult>> ListAsync<TResult>(
        Specification<TEntity, TResult> specification,
        CancellationToken cancellationToken = default) =>
        await ApplySpecification(specification).ToListAsync(cancellationToken);

    public async Task<List<TEntity>> ListAsync(
        Specification<TEntity> specification,
        CancellationToken cancellationToken = default) =>
        await ApplySpecification(specification).ToListAsync(cancellationToken);

    public void Remove(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);

    public void RemoveRange(IEnumerable<TEntity> entities) =>
        _dbContext.Set<TEntity>().RemoveRange(entities);

    public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

    public void UpdateRange(IEnumerable<TEntity> entities) =>
        _dbContext.Set<TEntity>().UpdateRange(entities);

    private IQueryable<TEntity> ApplySpecification(Specification<TEntity> specification)
    {
        var query = _dbContext.Set<TEntity>().AsQueryable();

        // Include evaluator
        foreach (var include in specification.IncludeExpressions)
        {
            query = query.Include(include);
        }

        // Where evaluator
        foreach (var where in specification.WhereExpressions)
        {
            query = query.Where(where);
        }

        return query;
    }

    private IQueryable<TResult> ApplySpecification<TResult>(Specification<TEntity, TResult> specification) =>
        throw new NotImplementedException();
}