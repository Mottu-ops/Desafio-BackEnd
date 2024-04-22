namespace Motorent.Application.Common.Abstractions.Security.Authorization;

internal interface IAuthorizationRequirementHandler
{
    Task<AuthorizationResult> HandleAsync(object requirement, CancellationToken cancellationToken = default);
}

internal interface IAuthorizationRequirementHandler<in TRequirement>
    : IAuthorizationRequirementHandler where TRequirement : IAuthorizationRequirement
{
    Task<AuthorizationResult> HandleAsync(TRequirement requirement,
        CancellationToken cancellationToken = default);

    Task<AuthorizationResult> IAuthorizationRequirementHandler.HandleAsync(object requirement,
        CancellationToken cancellationToken) => HandleAsync((TRequirement)requirement, cancellationToken);
}