using RichillCapital.Exchange.Api.Extensions;

var app = WebApplication
    .CreateBuilder(args)
    .ConfigureServices()
    .Build();

await (await app.ConfigurePipelinesAsync())
    .RunAsync();

public partial class Program;