using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

using RichillCapital.Domain.Common;
using RichillCapital.SharedKernel.Specifications.Evaluators;

namespace RichillCapital.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddPersistenceOptions();

        services.AddPostgreSql();

        return services;
    }

    private static IServiceCollection AddPersistenceOptions(this IServiceCollection services)
    {
        services
            .AddOptions<PersistenceOptions>()
            .BindConfiguration(nameof(PersistenceOptions))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }

    private static IServiceCollection AddPostgreSql(this IServiceCollection services)
    {
        services.AddDbContext<PostgreSqlDbContext>((servicesProvider, options) =>
        {
            var postgreSqlOptions = servicesProvider
                .GetRequiredService<IOptions<PersistenceOptions>>()
                .Value.PostgreSqlOptions;

            options.UseNpgsql(postgreSqlOptions.ConnectionString);
        });

        services.AddScoped(typeof(IRepository<>), typeof(PostgresRepository<>));
        services.AddScoped(typeof(IReadOnlyRepository<>), typeof(PostgresRepository<>));
        services.AddScoped<IUnitOfWork>(serviceProvider =>
                serviceProvider.GetRequiredService<PostgreSqlDbContext>());

        services.AddScoped<IInMemorySpecificationEvaluator, InMemorySpecificationEvaluator>();
        return services;
    }

}