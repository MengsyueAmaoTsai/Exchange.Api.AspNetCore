using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

using RichillCapital.Domain.Common;

namespace RichillCapital.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services
            .AddOptions<PersistenceOptions>()
            .BindConfiguration(nameof(PersistenceOptions))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddPostgreSql();

        return services;
    }

    private static IServiceCollection AddPostgreSql(this IServiceCollection services)
    {
        services.AddDbContext<PostgreSqlOptionsDbContext>((servicesProvider, options) =>
        {
            var postgreSqlOptions = servicesProvider
                .GetRequiredService<IOptions<PersistenceOptions>>()
                .Value.PostgreSqlOptions;

            options.UseNpgsql(postgreSqlOptions.ConnectionString);
        });

        services.AddScoped(typeof(IRepository<>), typeof(PostgresRepository<>));
        services.AddScoped(typeof(IReadOnlyRepository<>), typeof(PostgresRepository<>));
        services.AddScoped<IUnitOfWork>(serviceProvider =>
                serviceProvider.GetRequiredService<PostgreSqlOptionsDbContext>());

        return services;
    }

}