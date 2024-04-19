using Motorent.Domain.Users;

namespace Motorent.Application.Common.Abstractions.Security;

public interface ISecurityTokenService
{
    Task<SecurityToken> GenerateTokenAsync(User user, CancellationToken cancellationToken = default);
    
    Task<Result<SecurityToken>> RefreshTokenAsync(
        string accessToken,
        string refreshToken,
        CancellationToken cancellationToken = default);
}