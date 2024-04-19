using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Motorent.Application.Common.Abstractions.Security;
using Motorent.Domain.Users;
using Motorent.Domain.Users.ValueObjects;
using Motorent.Infrastructure.Common.Identity;
using Motorent.Infrastructure.Common.Persistence;
using ResultExtensions;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using SecurityToken = Motorent.Application.Common.Abstractions.Security.SecurityToken;

namespace Motorent.Infrastructure.Common.Security;

internal sealed class SecurityTokenService(
    DataContext dataContext,
    TimeProvider timeProvider,
    IOptions<SecurityTokenOptions> options)
    : ISecurityTokenService
{
    private const string Algorithm = SecurityAlgorithms.HmacSha256;

    private readonly SecurityTokenOptions options = options.Value;

    public async Task<SecurityToken> GenerateTokenAsync(User user)
    {
        var accessTokenId = Guid.NewGuid().ToString();
        var accessToken = GenerateAccessToken(user, accessTokenId);
        var refreshToken = await GenerateRefreshTokenAsync(user.Id, accessTokenId);

        return new SecurityToken(
            AccessToken: accessToken,
            RefreshToken: refreshToken,
            ExpiresIn: options.ExpiresInMinutes);
    }

    public async Task<Result<SecurityToken>> RefreshTokenAsync(
        string accessToken,
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        var principal = GetPrincipalFromExpiredAccessToken(accessToken);
        var userId = principal.FindFirstValue(JwtRegisteredClaimNames.Sub) is { } sub
            ? new UserId(Guid.Parse(sub))
            : throw new InvalidOperationException(
                $"Missing claim '{JwtRegisteredClaimNames.Sub}' in the access token");

        var jti = principal.FindFirstValue(JwtRegisteredClaimNames.Jti);

        var refreshTokenEntity = await dataContext.Set<RefreshToken>()
            .FindAsync([userId, refreshToken], cancellationToken);

        if (refreshTokenEntity is null || refreshTokenEntity.AccessTokenId != jti)
        {
            return SecurityTokenErrors.InvalidRefreshToken;
        }

        if (refreshTokenEntity.IsRevoked)
        {
            return SecurityTokenErrors.RefreshTokenRevoked;
        }

        if (refreshTokenEntity.HasExpired(timeProvider.GetUtcNow()))
        {
            return SecurityTokenErrors.RefreshTokenExpired;
        }

        if (refreshTokenEntity.IsUsed)
        {
            return SecurityTokenErrors.RefreshTokenUsed;
        }

        var user = await dataContext.Set<User>()
            .FindAsync([userId], cancellationToken);

        if (user is null)
        {
            return SecurityTokenErrors.InvalidRefreshToken;
        }

        var newAccessToken = GenerateAccessToken(user, jti);
        var secureToken = new SecurityToken(
            newAccessToken,
            refreshToken, // Reuse the same refresh token
            options.ExpiresInMinutes);

        refreshTokenEntity.MarkAsUsed(timeProvider.GetUtcNow());
        await dataContext.SaveChangesAsync(cancellationToken);

        return secureToken;
    }

    private string GenerateAccessToken(User user, string accessTokenId)
    {
        Claim[] claims =
        [
            new Claim(JwtRegisteredClaimNames.Jti, accessTokenId),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimsPrincipalExtensions.RoleClaimType, user.Role.Name),
            new Claim(JwtRegisteredClaimNames.Name, user.Name),
            new Claim(JwtRegisteredClaimNames.Birthdate, user.Birthdate.ToString("yyyy-MM-dd"))
        ];

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key)), Algorithm);

        var now = timeProvider.GetUtcNow().UtcDateTime;
        var expires = now.AddMinutes(options.ExpiresInMinutes);

        var securityToken = new JwtSecurityToken(
            issuer: options.Issuer,
            audience: options.Audience,
            claims: claims,
            notBefore: now,
            expires: expires,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(securityToken);
    }

    private async Task<string> GenerateRefreshTokenAsync(UserId userId, string accessTokenId)
    {
        var refreshToken = RefreshToken.Create(
            userId,
            accessTokenId,
            timeProvider.GetUtcNow().AddMinutes(options.RefreshTokenExpiresInMinutes));

        await dataContext.Set<RefreshToken>()
            .AddAsync(refreshToken);

        await dataContext.SaveChangesAsync();

        return refreshToken.Token;
    }

    private ClaimsPrincipal GetPrincipalFromExpiredAccessToken(string token)
    {
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key))
        };

        var handler = new JwtSecurityTokenHandler();
        var principal = handler.ValidateToken(token, validationParameters, out var securityToken);

        return securityToken is not JwtSecurityToken { Header.Alg: Algorithm }
            ? throw new SecurityTokenException("Invalid access token")
            : principal;
    }
}