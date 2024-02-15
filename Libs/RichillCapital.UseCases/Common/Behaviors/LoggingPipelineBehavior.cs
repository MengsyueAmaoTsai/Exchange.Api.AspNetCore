using System.Diagnostics;

using MediatR;

using Microsoft.Extensions.Logging;

namespace RichillCapital.UseCases.Common.Behaviors;

internal sealed class LoggingPipelineBehavior<TRequest, TResult>(
    ILogger<LoggingPipelineBehavior<TRequest, TResult>> _logger) :
    IPipelineBehavior<TRequest, TResult>
    where TRequest : notnull
{
    public async Task<TResult> Handle(
        TRequest request,
        RequestHandlerDelegate<TResult> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        try
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Handling {RequestName}", requestName);

                foreach (var property in request.GetType().GetProperties())
                {
                    object? value = property.GetValue(request, null);

                    _logger.LogInformation("Property {Property} : {@Value}", property.Name, value);
                }
            }

            var stopwatch = Stopwatch.StartNew();

            var response = await next();

            _logger.LogInformation(
                "Handled {RequestName} with {Response} in {ms} ms",
                typeof(TRequest).Name,
                response,
                stopwatch.ElapsedMilliseconds);

            stopwatch.Stop();

            return response;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "{RequestName} processing failed.", requestName);

            throw;
        }
    }
}