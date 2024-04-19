using Motorent.Application.Common.Abstractions.Requests;
using Motorent.Application.Common.Abstractions.Security;
using Motorent.Application.Users.Common.Errors;
using Motorent.Contracts.Users.Responses;
using Motorent.Domain.Users.Repository;
using Motorent.Domain.Users.Services;

namespace Motorent.Application.Users.Commands.Login;

internal sealed class LoginCommandHandler(
    IUserRepository userRepository,
    IEncryptionService encryptionService,
    ISecurityTokenService securityTokenService) : ICommandHandler<LoginCommand, TokenResponse>
{
    public async Task<Result<TokenResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByEmailAsync(command.Email, cancellationToken);
        if (user is null || !encryptionService.Verify(command.Password, user.PasswordHash))
        {
            return UserErrors.InvalidCredentials;
        }

        return (await securityTokenService.GenerateTokenAsync(user, cancellationToken))
            .Adapt<TokenResponse>();
    }
}