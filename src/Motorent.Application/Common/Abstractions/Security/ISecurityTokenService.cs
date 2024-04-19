using Motorent.Domain.Users;

namespace Motorent.Application.Common.Abstractions.Security;

public interface ISecurityTokenService
{
    Task<SecurityToken> GenerateTokenAsync(User user);
    
    Task<Result<SecurityToken>> RefreshTokenAsync(
        string accessToken,
        string refreshToken,
        CancellationToken cancellationToken = default);
}