using Microsoft.AspNetCore.Http;
using Motorent.Application.Common.Abstractions.Identity;
using Motorent.Domain.Users.Enums;
using Motorent.Domain.Users.ValueObjects;

namespace Motorent.Infrastructure.Common.Identity;

internal sealed class UserContext : IUserContext
{
    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;
        IsAuthenticated = user?.Identity?.IsAuthenticated ?? false;

        UserId = IsAuthenticated ? user!.GetUserId() : new UserId(Guid.Empty);
        Role = IsAuthenticated ? user!.GetRole() : Role.None;
    }

    public bool IsAuthenticated { get; }

    public UserId UserId { get; }

    public Role Role { get; }
}