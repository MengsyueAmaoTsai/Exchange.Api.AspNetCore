using Microsoft.OpenApi.Models;

using RichillCapital.Exchange.Api.Middlewares;

namespace RichillCapital.Exchange.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMiddlewares();
        services.AddEndpoints();
        services.AddOpenApiDocumentation();
        services.AddCustomCorsPolicy();

        return services;
    }

    private static IServiceCollection AddCustomCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options => options.AddPolicy(
            "default",
            builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));

        return services;
    }

    private static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddProblemDetails();
        services.AddHealthChecks();

        return services;
    }

    private static IServiceCollection AddMiddlewares(this IServiceCollection services)
    {
        services.AddTransient<RequestContextLoggingMiddleware>();

        return services;
    }

    private static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services)
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

    public static IServiceCollection AddHealthChecks(this IServiceCollection services)
    {
        return services;
    }
}