using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using RichillCapital.Domain.Common;
using RichillCapital.SharedKernel;

namespace RichillCapital.Persistence;

public sealed class PostgreSqlOptionsDbContext(
    DbContextOptions<PostgreSqlOptionsDbContext> options,
    IDomainEventDispatcher _domainEventDispatcher) :
    DbContext(options), IUnitOfWork
{
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int result = await base.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        if (_domainEventDispatcher is null)
        {
            return result;
        }

        var entitiesWithEvents = ChangeTracker.Entries<IEntity>()
            .Select(e => e.Entity)
            .Where(e => e.GetDomainEvents().Any())
            .ToArray();

        await _domainEventDispatcher.DispatchAndClearDomainEvents(entitiesWithEvents);

        return result;
    }

    public override int SaveChanges() => SaveChangesAsync().GetAwaiter().GetResult();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgreSqlOptionsDbContext).Assembly);
    }
}