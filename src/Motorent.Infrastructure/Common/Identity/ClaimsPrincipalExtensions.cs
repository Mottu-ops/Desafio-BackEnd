using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Motorent.Domain.Users.Enums;
using Motorent.Domain.Users.ValueObjects;

namespace Motorent.Infrastructure.Common.Identity;

internal static class ClaimsPrincipalExtensions
{
    internal const string RoleClaimType = "role";
    
    public static UserId GetUserId(this ClaimsPrincipal principal)
    {
        var sub = principal.FindFirstValue(JwtRegisteredClaimNames.Sub);
        return Guid.TryParse(sub, out var guid)
            ? new UserId(guid)
            : throw new InvalidOperationException("User ID claim is missing or invalid");
    }

    public static Role GetRole(this ClaimsPrincipal principal)
    {
        var name = principal.FindFirstValue(RoleClaimType);
        return Role.TryFromName(name, out var role)
            ? role
            : throw new InvalidOperationException("Role claim is missing or invalid");
    }
}