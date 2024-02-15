using RichillCapital.Caching;
using RichillCapital.Exchange.Api.Middlewares;
using RichillCapital.Notifications;
using RichillCapital.Persistence;
using RichillCapital.UseCases;

using Serilog;

namespace RichillCapital.Exchange.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(builder.Configuration));

        builder.Services.AddUseCases();

        builder.Services.AddPresentation();

        return builder;
    }

    public static async Task<WebApplication> ConfigurePipelinesAsync(this WebApplication app)
    {
        app.UseRequestContextLogging();
        app.UseSerilogRequestLogging();

        // app.InitializeSeeds();
        // await app.InitializeDataFeedsAsync();

        app.UseCors("default");
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.MapControllers();

        return app;
    }

    private static WebApplication UseRequestContextLogging(this WebApplication app)
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();

        return app;
    }

    // private static WebApplication InitializeSeeds(this WebApplication app)
    // {
    //     using var scope = app.Services.CreateScope();
    //     var services = scope.ServiceProvider;
    //     var logger = services.GetRequiredService<ILogger<Program>>();

    //     try
    //     {
    //         var context = services.GetRequiredService<PostgreSqlOptionsDbContext>();
    //         context.Database.EnsureDeleted();
    //         context.Database.EnsureCreated();
    //         Seeds.Initialize(services);

    //         logger.LogInformation("Seed populated successfully.");
    //     }
    //     catch (Exception ex)
    //     {
    //         logger.LogError(ex, "An error occurred seeding the database. {exceptionMessage}", ex.Message);
    //     }

    //     return app;
    // }

    // private static async Task<WebApplication> InitializeDataFeedsAsync(this WebApplication app)
    // {
    //     using var scope = app.Services.CreateScope();

    //     var dataFeedService = scope.ServiceProvider.GetRequiredService<DataFeedProvider>();
    //     await dataFeedService.InitializeAsync();
    //     return app;
    // }
}