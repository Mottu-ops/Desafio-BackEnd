using Motorent.Application.Common.Abstractions.Security;
using Motorent.Application.Users.Commands.Refresh;

namespace Motorent.Application.UnitTests.Users.Commands.Refresh;

[TestSubject(typeof(RefreshTokenCommandHandler))]
public sealed class RefreshTokenCommandHandlerTests
{
    private readonly ISecurityTokenService securityTokenService = A.Fake<ISecurityTokenService>();
    private readonly CancellationToken cancellationToken = A.Dummy<CancellationToken>();

    private readonly RefreshTokenCommand command = new()
    {
        AccessToken = "access-token",
        RefreshToken = "refresh-token"
    };

    private readonly RefreshTokenCommandHandler sut;

    public RefreshTokenCommandHandlerTests()
    {
        sut = new RefreshTokenCommandHandler(securityTokenService);
    }

    [Fact]
    public async Task Handle_WhenTokenIsRefreshed_ShouldReturnTokenResponse()
    {
        // Arrange
        var securityToken = new SecurityToken(
            AccessToken: "access-token",
            RefreshToken: "refresh-token",
            ExpiresIn: 3600);

        A.CallTo(() => securityTokenService.RefreshTokenAsync(
                command.AccessToken,
                command.RefreshToken,
                cancellationToken))
            .Returns(securityToken);

        // Act
        var result = await sut.Handle(command, cancellationToken);

        // Assert
        result.Should().BeSuccess()
            .Which.Value.Should().BeEquivalentTo(new
            {
                securityToken.TokenType,
                securityToken.AccessToken,
                securityToken.RefreshToken,
                securityToken.ExpiresIn
            });
    }
    
    [Fact]
    public async Task Handle_WhenTokenIsNotRefreshed_ShouldReturnFailure()
    {
        // Arrange
        A.CallTo(() => securityTokenService.RefreshTokenAsync(
                command.AccessToken,
                command.RefreshToken,
                cancellationToken))
            .Returns(SecurityTokenErrors.InvalidRefreshToken);

        // Act
        var result = await sut.Handle(command, cancellationToken);

        // Assert
        result.Should().BeFailure();
    }
}