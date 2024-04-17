using FluentAssertions;
using Motorent.Application.Common.Abstractions.Security;
using Motorent.Application.Users.Commands.Register;
using Motorent.Contracts.Users.Responses;
using Motorent.Domain.Users;
using Motorent.Domain.Users.Repository;
using Motorent.Domain.Users.Services;
using Motorent.TestUtils.Constants;

namespace Motorent.Application.UnitTests.Users.Commands.Register;

[TestSubject(typeof(RegisterCommandHandler))]
public sealed class RegisterCommandHandlerTests
{
    private readonly IUserRepository userRepository = A.Fake<IUserRepository>();
    private readonly IEncryptionService encryptionService = A.Fake<IEncryptionService>();
    private readonly IEmailUniquenessChecker emailUniquenessChecker = A.Fake<IEmailUniquenessChecker>();
    private readonly ISecurityTokenService securityTokenService = A.Fake<ISecurityTokenService>();

    private readonly CancellationToken cancellationToken = A.Dummy<CancellationToken>();

    private readonly RegisterCommand command = new()
    {
        Name = Constants.User.Name,
        Email = Constants.User.Email,
        Password = Constants.User.Password,
        Birthdate = Constants.User.Birthdate
    };

    private readonly RegisterCommandHandler sut;

    public RegisterCommandHandlerTests()
    {
        sut = new RegisterCommandHandler(
            userRepository,
            encryptionService,
            emailUniquenessChecker,
            securityTokenService);

        A.CallTo(() => emailUniquenessChecker.IsUniqueAsync(command.Email, cancellationToken))
            .Returns(true);

        A.CallTo(() => encryptionService.Encrypt(command.Password))
            .Returns("hashed-password");
    }

    [Fact]
    public async Task Handle_WhenUserCreationSucceeds_ShouldAddToRepository()
    {
        // Arrange
        // Act
        await sut.Handle(command, cancellationToken);

        // Assert
        A.CallTo(() => userRepository.AddAsync(
                A<User>.That.Matches(u => u.Name == command.Name
                                          && u.Email == command.Email
                                          && u.PasswordHash == "hashed-password"
                                          && u.Birthdate == command.Birthdate),
                cancellationToken))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_WhenUserCreationSucceeds_ShouldReturnTokenResponse()
    {
        // Arrange
        var securityToken = new SecurityToken(
            AccessToken: "access-token",
            ExpiresIn: 5);

        A.CallTo(() => securityTokenService.GenerateTokenAsync(A<User>._))
            .Returns(securityToken);

        // Act
        var result = await sut.Handle(command, cancellationToken);

        // Assert
        result.Should().BeSuccess()
            .Which.Value.Should().BeOfType<TokenResponse>()
            .Which.Should().BeEquivalentTo(new
            {
                securityToken.AccessToken,
                securityToken.ExpiresIn
            });
    }

    [Fact]
    public async Task Handle_WhenUserCreationFails_ShouldReturnFailure()
    {
        // Arrange
        A.CallTo(() => emailUniquenessChecker.IsUniqueAsync(command.Email, cancellationToken))
            .Returns(false);

        // Act
        var result = await sut.Handle(command, cancellationToken);

        // Assert
        result.Should().BeFailure();
    }

    [Fact]
    public async Task Handle_WhenUserCreationFails_ShouldNotAddToRepository()
    {
        // Arrange
        A.CallTo(() => emailUniquenessChecker.IsUniqueAsync(command.Email, cancellationToken))
            .Returns(false);

        // Act
        await sut.Handle(command, cancellationToken);

        // Assert
        A.CallTo(() => userRepository.AddAsync(A<User>._, cancellationToken))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task Handle_WhenUserCreationFails_ShouldNotGenerateToken()
    {
        // Arrange
        A.CallTo(() => emailUniquenessChecker.IsUniqueAsync(command.Email, cancellationToken))
            .Returns(false);

        // Act
        await sut.Handle(command, cancellationToken);

        // Assert
        A.CallTo(() => securityTokenService.GenerateTokenAsync(A<User>._))
            .MustNotHaveHappened();
    }
}