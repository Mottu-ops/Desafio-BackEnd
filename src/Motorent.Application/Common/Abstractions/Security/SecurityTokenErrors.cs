namespace Motorent.Application.Common.Abstractions.Security;

public static class SecurityTokenErrors
{
    public static readonly Error InvalidRefreshToken = Error.Unauthorized("Invalid refresh token.");
    public static readonly Error RefreshTokenUsed = Error.Unauthorized("Refresh token has already been used.");
    public static readonly Error RefreshTokenExpired = Error.Unauthorized("Refresh token has expired.");
    public static readonly Error RefreshTokenRevoked = Error.Unauthorized("Refresh token has been revoked.");
}