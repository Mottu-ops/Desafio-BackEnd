using Microsoft.Extensions.Logging;

namespace Motorent.Application.Common.Behaviors;

internal sealed class ExceptionBehavior<TRequest, TResponse>(
    ILogger<ExceptionBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : IResult
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhanded exception occurred while processing the request {RequestName}",
                typeof(TRequest).Name);

            return (TResponse)(dynamic)Error.Unexpected("An internal error occurred");
        }
    }
}