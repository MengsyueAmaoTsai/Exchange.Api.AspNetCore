using Microsoft.Extensions.Primitives;

using Serilog.Context;

namespace RichillCapital.Exchange.Api.Middlewares;

public sealed class RequestContextLoggingMiddleware : IMiddleware
{
    private const string CorrelationIdHeaderName = "X-Correlation-Id";

    public Task InvokeAsync(
        HttpContext context,
        RequestDelegate next)
    {
        string correlationId = GetCorrelationId(context);

        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            return next.Invoke(context);
        }
    }

    private static string GetCorrelationId(HttpContext context)
    {
        context.Request.Headers.TryGetValue(
            CorrelationIdHeaderName,
            out StringValues correlationId);

        return correlationId.FirstOrDefault() ??
            context.TraceIdentifier;
    }
}