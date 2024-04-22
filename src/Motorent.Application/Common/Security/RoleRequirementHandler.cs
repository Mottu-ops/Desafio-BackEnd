using Motorent.Application.Common.Abstractions.Identity;

namespace Motorent.Application.Common.Security;

internal sealed class RoleRequirementHandler(IUserContext userContext)
    : IAuthorizationRequirementHandler<RoleRequirement>
{
    public Task<AuthorizationResult> HandleAsync(RoleRequirement requirement,
        CancellationToken cancellationToken = default)
    {
        var result = requirement.RequiredRole == userContext.Role
            ? AuthorizationResult.Success()
            : AuthorizationResult.Failure($"Role requirement not met. Required role: {requirement.RequiredRole}");

        return Task.FromResult(result);
    }
}