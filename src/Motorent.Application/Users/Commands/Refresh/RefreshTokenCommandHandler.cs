using Motorent.Application.Common.Abstractions.Requests;
using Motorent.Application.Common.Abstractions.Security;
using Motorent.Contracts.Users.Responses;

namespace Motorent.Application.Users.Commands.Refresh;

internal sealed class RefreshTokenCommandHandler(ISecurityTokenService securityTokenService)
    : ICommandHandler<RefreshTokenCommand, TokenResponse>
{
    public async Task<Result<TokenResponse>> Handle(RefreshTokenCommand command,
        CancellationToken cancellationToken)
    {
        var result = await securityTokenService.RefreshTokenAsync(
            command.AccessToken,
            command.RefreshToken,
            cancellationToken);

        return result.Then(securityToken=> securityToken.Adapt<TokenResponse>());
    }
}