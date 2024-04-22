using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using Motorent.Application.Common.Abstractions.Security;
using Motorent.Domain.Users;
using Motorent.Domain.Users.ValueObjects;
using Motorent.Infrastructure.Common.Identity;
using Motorent.Infrastructure.Common.Persistence;
using ResultExtensions;
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

    public async Task<SecurityToken> GenerateTokenAsync(User user, CancellationToken cancellationToken = default)
    {
        var accessTokenId = Guid.NewGuid().ToString();
        var accessToken = GenerateAccessToken(user, accessTokenId);
        var refreshToken = await GenerateRefreshTokenAsync(user.Id, accessTokenId, cancellationToken);

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
        var claims = await GetClaimsFromExpiredGetAccessToken(accessToken);
        var sub = claims.Single(c => c.Key == JwtRegisteredClaimNames.Sub).Value!;
        var jti = claims.SingleOrDefault(c => c.Key == JwtRegisteredClaimNames.Jti).Value!;

        var userId = new UserId(Guid.Parse(sub));
        
        var user = await dataContext.Set<User>()
            .FindAsync([userId], cancellationToken);
        
        var refreshTokenEntity = await dataContext.Set<RefreshToken>()
            .FindAsync([userId, refreshToken], cancellationToken);

        if (user is null || refreshTokenEntity is null || refreshTokenEntity.AccessTokenId != jti)
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
        var claims = new Dictionary<string, object>
        {
            { JwtRegisteredClaimNames.Jti, accessTokenId },
            { JwtRegisteredClaimNames.Sub, user.Id.ToString() },
            { ClaimsPrincipalExtensions.RoleClaimType, user.Role.Name },
            { JwtRegisteredClaimNames.Name, user.Name.Value },
            { JwtRegisteredClaimNames.Birthdate, user.Birthdate.ToString("yyyy-MM-dd") }
        };

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key)), Algorithm);

        var now = timeProvider.GetUtcNow().UtcDateTime;
        var expires = now.AddMinutes(options.ExpiresInMinutes);

        var descriptor = new SecurityTokenDescriptor
        {
            Expires = expires,
            NotBefore = now,
            Claims = claims,
            Issuer = options.Issuer,
            Audience = options.Audience,
            SigningCredentials = credentials
        };

        var handler = new JsonWebTokenHandler
        {
            MapInboundClaims = false,
            SetDefaultTimesOnTokenCreation = false
        };

        return handler.CreateToken(descriptor);
    }

    private async Task<string> GenerateRefreshTokenAsync(
        UserId userId,
        string accessTokenId,
        CancellationToken cancellationToken)
    {
        var refreshToken = RefreshToken.Create(
            userId,
            accessTokenId,
            timeProvider.GetUtcNow().AddMinutes(options.RefreshTokenExpiresInMinutes));

        await dataContext.Set<RefreshToken>()
            .AddAsync(refreshToken, cancellationToken);

        await dataContext.SaveChangesAsync(cancellationToken);

        return refreshToken.Token;
    }

    private async Task<IDictionary<string, string?>> GetClaimsFromExpiredGetAccessToken(string accessToken)
    {
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key))
        };

        var handler = new JsonWebTokenHandler();
        var result = await handler.ValidateTokenAsync(accessToken, validationParameters);

        return result.Claims.ToDictionary(c => c.Key, c => c.Value?.ToString());
    }
}