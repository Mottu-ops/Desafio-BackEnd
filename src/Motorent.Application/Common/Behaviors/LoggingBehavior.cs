using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Motorent.Application.Common.Behaviors;

internal sealed class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = $"{typeof(TRequest).Name} - {Guid.NewGuid()}";
        var stopwatch = new Stopwatch();

        logger.LogInformation("Handling {RequestName} ({@Request})", requestName, request);

        stopwatch.Start();

        var response = await next();

        stopwatch.Stop();

        logger.LogInformation("Handled {RequestName} in {ElapsedMilliseconds}ms",
            requestName, stopwatch.ElapsedMilliseconds);

        return response;
    }
}