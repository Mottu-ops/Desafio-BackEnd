using Motorent.Application.Common.Abstractions.Security;
using Motorent.Application.Users.Commands.Login;
using Motorent.Application.Users.Common.Errors;
using Motorent.Domain.Users;
using Motorent.Domain.Users.Repository;
using Motorent.Domain.Users.Services;

namespace Motorent.Application.UnitTests.Users.Commands.Login;

[TestSubject(typeof(LoginCommandHandler))]
public sealed class LoginCommandHandlerTests
{
    private readonly IUserRepository userRepository = A.Fake<IUserRepository>();
    private readonly IEncryptionService encryptionService = A.Fake<IEncryptionService>();
    private readonly ISecurityTokenService securityTokenService = A.Fake<ISecurityTokenService>();
    private readonly CancellationToken cancellationToken = A.Dummy<CancellationToken>();

    private readonly LoginCommand command = new()
    {
        Email = Constants.User.Email,
        Password = Constants.User.Password
    };

    private readonly LoginCommandHandler sut;

    public LoginCommandHandlerTests()
    {
        sut = new LoginCommandHandler(userRepository, encryptionService, securityTokenService);
    }

    [Fact]
    public async Task Handle_WhenUserDoesNotExists_ShouldReturnInvalidCredentials()
    {
        // Arrange
        A.CallTo(() => userRepository.FindByEmailAsync(command.Email, cancellationToken))
            .Returns(null as User);

        // Act
        var result = await sut.Handle(command, cancellationToken);

        // Assert
        result.Should().BeFailure(UserErrors.InvalidCredentials);
    }

    [Fact]
    public async Task Handle_WhenPasswordIsIncorrect_ShouldReturnInvalidCredentials()
    {
        // Arrange
        var user = (await Factories.User.CreateUserAsync()).Value;

        A.CallTo(() => userRepository.FindByEmailAsync(command.Email, cancellationToken))
            .Returns(user);

        A.CallTo(() => encryptionService.Verify(command.Password, user.PasswordHash))
            .Returns(false);

        // Act
        var result = await sut.Handle(command, cancellationToken);

        // Assert
        result.Should().BeFailure(UserErrors.InvalidCredentials);
    }

    [Fact]
    public async Task Handle_WhenUserExistsAndPasswordIsCorrect_ShouldReturnTokenResponse()
    {
        // Arrange
        var user = (await Factories.User.CreateUserAsync()).Value;

        A.CallTo(() => userRepository.FindByEmailAsync(command.Email, cancellationToken))
            .Returns(user);

        A.CallTo(() => encryptionService.Verify(command.Password, user.PasswordHash))
            .Returns(true);

        var securityToken = new SecurityToken(
            TokenType: "Bearer",
            AccessToken: "access-token",
            ExpiresIn: 3600);

        A.CallTo(() => securityTokenService.GenerateTokenAsync(user))
            .Returns(securityToken);

        // Act
        var result = await sut.Handle(command, cancellationToken);

        // Assert
        result.Should().BeSuccess()
            .Which.Value.Should().BeEquivalentTo(new
            {
                securityToken.TokenType,
                securityToken.AccessToken,
                securityToken.ExpiresIn
            });
    }
}