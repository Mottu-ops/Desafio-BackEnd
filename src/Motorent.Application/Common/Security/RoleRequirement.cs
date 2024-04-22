using Motorent.Domain.Users.Enums;

namespace Motorent.Application.Common.Security;

internal sealed class RoleRequirement(Role requiredRole) : IAuthorizationRequirement
{
    public Role RequiredRole { get; } = requiredRole;
}