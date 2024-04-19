using System.Security.Cryptography;
using Motorent.Domain.Users.ValueObjects;

namespace Motorent.Infrastructure.Common.Security;

internal sealed class RefreshToken
{
    private RefreshToken()
    {
    }
    
    public required UserId UserId { get; init; }

    public required string Token { get; init; }

    public required string AccessTokenId { get; init; }

    public required DateTimeOffset Expires { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }
    
    public DateTimeOffset? UsedAt { get; private set; }

    public DateTimeOffset? RevokedAt { get; private set; }
    
    public bool IsUsed => UsedAt.HasValue;

    public bool IsRevoked => RevokedAt.HasValue;

    public static RefreshToken Create(UserId userId, string accessTokenId, DateTimeOffset expires)
    {
        var tokenBuffer = new byte[64];
        RandomNumberGenerator.Fill(tokenBuffer);

        return new RefreshToken
        {
            UserId = userId,
            Token = Convert.ToBase64String(tokenBuffer),
            AccessTokenId = accessTokenId,
            Expires = expires,
            CreatedAt = DateTimeOffset.UtcNow
        };
    }
    
    public void MarkAsUsed(DateTimeOffset now) => UsedAt = now;
    
    public void MarkAsRevoked(DateTimeOffset now) => RevokedAt = now;
    
    public bool HasExpired(DateTimeOffset now) => Expires <= now;
}