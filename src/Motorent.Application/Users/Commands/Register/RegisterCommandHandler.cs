using Motorent.Application.Common.Abstractions.Requests;
using Motorent.Application.Common.Abstractions.Security;
using Motorent.Contracts.Users.Responses;
using Motorent.Domain.Common.ValueObjects;
using Motorent.Domain.Users;
using Motorent.Domain.Users.Enums;
using Motorent.Domain.Users.Repository;
using Motorent.Domain.Users.Services;
using Motorent.Domain.Users.ValueObjects;

namespace Motorent.Application.Users.Commands.Register;

internal sealed class RegisterCommandHandler(
    IUserRepository userRepository,
    IEncryptionService encryptionService,
    IEmailUniquenessChecker emailUniquenessChecker,
    ISecurityTokenService securityTokenService)
    : ICommandHandler<RegisterCommand, TokenResponse>
{
    public Task<Result<TokenResponse>> Handle(RegisterCommand command,
        CancellationToken cancellationToken)
    {
        var result = User.CreateAsync(
            id: UserId.New(),
            role: Role.FromName(command.Role),
            name: new Name(command.Name),
            email: command.Email,
            password: command.Password,
            birthdate: command.Birthdate,
            encryptionService: encryptionService,
            emailUniquenessChecker: emailUniquenessChecker,
            cancellationToken: cancellationToken);

        return result
            .ThenAsync(user => userRepository.AddAsync(user, cancellationToken))
            .ThenAsync(user => securityTokenService.GenerateTokenAsync(user, cancellationToken))
            .Then(securityToken => securityToken.Adapt<TokenResponse>());
    }
}