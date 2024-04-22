using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Motorent.Application.Common.Behaviors;

internal sealed class AuthorizationBehavior<TRequest, TResponse>(
    IServiceProvider serviceProvider,
    ILogger<AuthorizationBehavior<TRequest, TResponse>> logger,
    IAuthorizer<TRequest>? authorizer = null)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : class, IBaseRequest where TResponse : IResult
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (authorizer is null)
        {
            // A requisição não requer autorização
            return await next();
        }

        foreach (var requirement in authorizer.GetRequirements(request))
        {
            var handler = ResolveAuthorizationRequirementHandler(requirement.GetType(), serviceProvider);
            var result = await handler.HandleAsync(requirement, cancellationToken);

            if (result.Succeeded)
            {
                continue;
            }

            logger.LogInformation("Request {RequestName} was forbidden because: {Reason}",
                request.GetType().Name, result.Reason);

            return (TResponse)(dynamic)Error.Forbidden("You are not authorized to perform this action.");
        }

        return await next();
    }

    private static IAuthorizationRequirementHandler ResolveAuthorizationRequirementHandler(Type requirementType,
        IServiceProvider serviceProvider)
    {
        var requirementHandler = typeof(IAuthorizationRequirementHandler<>).MakeGenericType(requirementType);
        return serviceProvider.GetRequiredService(requirementHandler) as IAuthorizationRequirementHandler
               ?? throw new InvalidOperationException(
                   $"No authorization requirement handler found for '{requirementType.Name}'");
    }
}