using Microsoft.OpenApi.Models;

using RichillCapital.Exchange.Api.Middlewares;

namespace RichillCapital.Exchange.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddCors(options => options.AddPolicy(
            "default",
            builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));

        services.AddMiddlewares();
        services.AddControllers();
        services.AddProblemDetails();

        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Exchange.Api", Version = "v1" });
                options.EnableAnnotations();
            });

        return services;
    }

    private static IServiceCollection AddMiddlewares(this IServiceCollection services)
    {
        services.AddTransient<RequestContextLoggingMiddleware>();

        return services;
    }
}