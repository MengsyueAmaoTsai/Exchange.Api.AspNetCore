using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using RichillCapital.Domain.Common;
using RichillCapital.SharedKernel.Monad;
using RichillCapital.SharedKernel.Specifications;
using RichillCapital.SharedKernel.Specifications.Evaluators;

namespace RichillCapital.Persistence;

public class PostgresRepository<TEntity> :
    IRepository<TEntity>
    where TEntity : class
{
    private readonly PostgreSqlDbContext _dbContext;
    private readonly ISpecificationEvaluator _specificationEvaluator;

    public PostgresRepository(PostgreSqlDbContext dbContext)
    : this(dbContext, SpecificationEvaluator.Default)
    {
    }

    public PostgresRepository(
        PostgreSqlDbContext dbContext,
        ISpecificationEvaluator specificationEvaluator) =>
        (_dbContext, _specificationEvaluator) = (dbContext, specificationEvaluator);

    public void Add(TEntity entity) => _dbContext.Set<TEntity>().Add(entity);

    public void AddRange(IEnumerable<TEntity> entities) =>
        _dbContext.Set<TEntity>().AddRange(entities);

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.Set<TEntity>()
            .AnyAsync(cancellationToken);

    public Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default) =>
        _dbContext.Set<TEntity>()
            .AnyAsync(expression, cancellationToken);

    public async Task<bool> AnyAsync(
        ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default) =>
        await ApplySpecification(specification, true)
            .AnyAsync(cancellationToken);

    public IAsyncEnumerable<TEntity> AsAsyncEnumerable(
        ISpecification<TEntity> specification) =>
        ApplySpecification(specification).AsAsyncEnumerable();

    public async Task<int> CountAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.Set<TEntity>()
            .CountAsync(cancellationToken);

    public async Task<int> CountAsync(
        Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<TEntity>()
            .CountAsync(expression, cancellationToken);

    public async Task<int> CountAsync(
        ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default) =>
        await ApplySpecification(specification, true)
            .CountAsync(cancellationToken);

    public void RemoveRange(ISpecification<TEntity> specification)
    {
        var query = ApplySpecification(specification);
        _dbContext.Set<TEntity>().RemoveRange(query);
    }

    public async Task<Maybe<TEntity>> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<TEntity>()
            .FirstOrDefaultAsync(expression, cancellationToken);

    public async Task<Maybe<TEntity>> FirstOrDefaultAsync(
        ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default) =>
        await ApplySpecification(specification)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<Maybe<TResult>> FirstOrDefaultAsync<TResult>(
        ISpecification<TEntity, TResult> specification,
        CancellationToken cancellationToken = default) =>
        await ApplySpecification(specification)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<Maybe<TEntity>> GetByIdAsync<TId>(
        TId id,
        CancellationToken cancellationToken = default)
        where TId : notnull =>
        await _dbContext.Set<TEntity>()
            .FindAsync([id], cancellationToken);

    public Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default) =>
        _dbContext.Set<TEntity>()
        .ToListAsync(cancellationToken);

    public async Task<List<TEntity>> ListAsync(
        Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<TEntity>()
            .Where(expression)
            .ToListAsync(cancellationToken);

    public async Task<List<TEntity>> ListAsync(
        ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default)
    {
        var queryResult = await ApplySpecification(specification)
            .ToListAsync(cancellationToken);

        return specification.PostProcessingAction is null ?
            queryResult :
            specification
                .PostProcessingAction(queryResult)
                .ToList();
    }

    public void Remove(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);

    public void RemoveRange(IEnumerable<TEntity> entities) =>
        _dbContext.Set<TEntity>().RemoveRange(entities);

    public async Task<Maybe<TEntity>> SingleOrDefaultAsync(
        ISingleResultSpecification<TEntity> specification,
        CancellationToken cancellationToken = default) =>
        await ApplySpecification(specification)
            .SingleOrDefaultAsync(cancellationToken);

    public async Task<Maybe<TResult>> SingleOrDefaultAsync<TResult>(
        ISingleResultSpecification<TEntity, TResult> specification,
        CancellationToken cancellationToken = default) =>
        await ApplySpecification(specification)
            .SingleOrDefaultAsync(cancellationToken);

    public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

    public void UpdateRange(IEnumerable<TEntity> entities) =>
        _dbContext.Set<TEntity>().UpdateRange(entities);

    protected virtual IQueryable<TEntity> ApplySpecification(
        ISpecification<TEntity> specification,
        bool evaluateCriteriaOnly = false) =>
        _specificationEvaluator.GetQuery(
            _dbContext.Set<TEntity>().AsQueryable(),
            specification,
            evaluateCriteriaOnly);

    protected virtual IQueryable<TResult> ApplySpecification<TResult>(
        ISpecification<TEntity, TResult> specification) =>
        _specificationEvaluator.GetQuery(
            _dbContext.Set<TEntity>().AsQueryable(),
            specification);
}


