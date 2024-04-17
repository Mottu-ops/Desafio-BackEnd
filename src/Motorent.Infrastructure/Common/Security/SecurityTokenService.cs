using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Motorent.Application.Common.Abstractions.Security;
using Motorent.Domain.Users;
using SecurityToken = Motorent.Application.Common.Abstractions.Security.SecurityToken;

namespace Motorent.Infrastructure.Common.Security;

internal sealed class SecurityTokenService(TimeProvider timeProvider, IOptions<SecurityTokenOptions> options)
    : ISecurityTokenService
{
    private const string Algorithm = SecurityAlgorithms.HmacSha256;

    private readonly SecurityTokenOptions options = options.Value;

    public Task<SecurityToken> GenerateTokenAsync(User user)
    {
        var accessTokenId = Guid.NewGuid().ToString();
        var accessToken = GenerateAccessToken(user, accessTokenId);

        return Task.FromResult(new SecurityToken(
            AccessToken: accessToken,
            ExpiresIn: options.ExpiresInMinutes));
    }

    private string GenerateAccessToken(User user, string accessTokenId)
    {
        Claim[] claims =
        [
            new Claim(JwtRegisteredClaimNames.Jti, accessTokenId),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
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
}