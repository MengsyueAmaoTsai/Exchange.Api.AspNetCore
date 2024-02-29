using System.Reflection;

using Mapster;

using MapsterMapper;

using Microsoft.OpenApi.Models;

using RichillCapital.Exchange.Api.Middlewares;

namespace RichillCapital.Exchange.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options => options.AddPolicy(
            "default",
            builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));

        return services;
    }

    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddProblemDetails();
        services.AddHealthChecks();
        services.AddMappings();

        return services;
    }

    public static IServiceCollection AddMiddlewares(this IServiceCollection services)
    {
        services.AddTransient<RequestContextLoggingMiddleware>();

        return services;
    }

    public static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Exchange.Api", Version = "v1" });
                options.EnableAnnotations();
            });

        return services;
    }

    private static IServiceCollection AddHealthChecks(this IServiceCollection services)
    {
        return services;
    }

    private static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}