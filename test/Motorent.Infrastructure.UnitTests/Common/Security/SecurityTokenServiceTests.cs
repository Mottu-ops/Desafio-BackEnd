using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Motorent.Application.Common.Abstractions.Security;
using Motorent.Domain.Users;
using Motorent.Infrastructure.Common.Identity;
using Motorent.Infrastructure.Common.Persistence;
using Motorent.Infrastructure.Common.Security;

namespace Motorent.Infrastructure.UnitTests.Common.Security;

[TestSubject(typeof(SecurityTokenService))]
public sealed class SecurityTokenServiceTests
{
    private readonly DataContext dataContext = A.Fake<DataContext>();

    private readonly DbSet<User> users = new List<User>().BuildMockDbSet();
    private readonly DbSet<RefreshToken> refreshTokens = new List<RefreshToken>().BuildMockDbSet();

    private readonly TimeProvider timeProvider = A.Fake<TimeProvider>(fakeOptions =>
        fakeOptions.Wrapping(TimeProvider.System));

    private readonly CancellationToken cancellationToken = A.Dummy<CancellationToken>();

    private readonly IOptions<SecurityTokenOptions> options = Options.Create(new SecurityTokenOptions
    {
        Key = "PePkSCXr0OMSgKCN06sB8sIRMXhhWQpB",
        Issuer = "localhost",
        Audience = "localhost",
        ExpiresInMinutes = 5
    });

    private readonly User user = Factories.User.CreateUserAsync()
        .Result
        .Value;

    private readonly SecurityTokenService sut;

    public SecurityTokenServiceTests()
    {
        sut = new SecurityTokenService(dataContext, timeProvider, options);

        A.CallTo(() => dataContext.Set<User>())
            .Returns(users);

        A.CallTo(() => dataContext.Set<RefreshToken>())
            .Returns(refreshTokens);
    }

    [Fact]
    public async Task GenerateTokenAsync_WhenCalled_ShouldReturnSecurityToken()
    {
        // Act
        var result = await sut.GenerateTokenAsync(user, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.TokenType.Should().NotBeNullOrWhiteSpace();
        result.AccessToken.Should().NotBeNullOrWhiteSpace();
        result.RefreshToken.Should().NotBeNullOrWhiteSpace();
        result.ExpiresIn.Should().Be(options.Value.ExpiresInMinutes);
    }

    [Fact]
    public async Task GenerateTokenAsync_WhenCalled_ShouldReturnSecurityTokenWithCorrectClaims()
    {
        // Act
        var result = await sut.GenerateTokenAsync(user, cancellationToken);
        var handler = new JsonWebTokenHandler();
        var token = handler.ReadJsonWebToken(result.AccessToken);

        var now = DateTimeOffset.UtcNow;

        A.CallTo(() => timeProvider.GetUtcNow())
            .Returns(now);

        // Assert
        token.Issuer.Should().Be(options.Value.Issuer);
        token.Audiences.Should().Contain(options.Value.Audience);
        token.ValidFrom.Should().BeCloseTo(now.UtcDateTime, TimeSpan.FromSeconds(5));
        token.ValidTo.Should().BeCloseTo(now.AddMinutes(
                options.Value.ExpiresInMinutes).UtcDateTime,
            TimeSpan.FromSeconds(5));

        token.Claims.Should().ContainSingle(claim =>
            claim.Type == JwtRegisteredClaimNames.Jti &&
            claim.ValueType == ClaimValueTypes.String &&
            claim.Value.Length == 36);

        token.Claims.Should().ContainSingle(claim =>
            claim.Type == JwtRegisteredClaimNames.Sub &&
            claim.ValueType == ClaimValueTypes.String &&
            claim.Value == user.Id.ToString());

        token.Claims.Should().ContainSingle(claim =>
            claim.Type == ClaimsPrincipalExtensions.RoleClaimType &&
            claim.ValueType == ClaimValueTypes.String &&
            claim.Value == user.Role.Name);

        token.Claims.Should().ContainSingle(claim =>
            claim.Type == JwtRegisteredClaimNames.Name &&
            claim.ValueType == ClaimValueTypes.String &&
            claim.Value == user.Name);

        token.Claims.Should().ContainSingle(claim =>
            claim.Type == JwtRegisteredClaimNames.Birthdate &&
            claim.ValueType == ClaimValueTypes.String &&
            claim.Value == user.Birthdate.ToString("yyyy-MM-dd"));
    }

    [Fact]
    public async Task GenerateTokenAsync_WhenCalled_ShouldSaveGeneratedRefreshToken()
    {
        // Arrange
        var handler = new JsonWebTokenHandler();

        // Act
        var result = await sut.GenerateTokenAsync(user, cancellationToken);

        // Assert
        var token = handler.ReadJsonWebToken(result.AccessToken);

        var jti = token.Claims.Single(claim =>
            claim.Type == JwtRegisteredClaimNames.Jti).Value;

        A.CallTo(() => refreshTokens
                .AddAsync(A<RefreshToken>.That.Matches(refreshToken =>
                        refreshToken.UserId == user.Id &&
                        refreshToken.AccessTokenId == jti),
                    A<CancellationToken>._))
            .MustHaveHappenedOnceExactly()
            .Then(A.CallTo(() => dataContext.SaveChangesAsync(A<CancellationToken>._))
                .MustHaveHappenedOnceExactly());
    }

    [Fact]
    public async Task RefreshTokenAsync_WhenCalled_ShouldReturnSecurityToken()
    {
        // Arrange
        A.CallTo(() => timeProvider.GetUtcNow())
            .Returns(DateTimeOffset.UtcNow);

        var securityToken = await sut.GenerateTokenAsync(user, cancellationToken);

        var tokenHandler = new JsonWebTokenHandler();
        var tokenClaims = tokenHandler.ReadJsonWebToken(securityToken.AccessToken);
        var jti = tokenClaims.Claims.Single(claim => claim.Type == JwtRegisteredClaimNames.Jti).Value;

        A.CallTo(() => users.FindAsync(new object?[] { user.Id }, cancellationToken))
            .Returns(user);

        var refreshToken = RefreshToken.Create(
            user.Id,
            jti,
            DateTimeOffset.UtcNow.AddDays(1));

        A.CallTo(() => refreshTokens.FindAsync(
                new object?[] { user.Id, securityToken.RefreshToken },
                cancellationToken))
            .Returns(refreshToken);

        A.CallTo(() => timeProvider.GetUtcNow())
            .Returns(DateTimeOffset.UtcNow.AddMinutes(1));

        // Act
        var result = await sut.RefreshTokenAsync(
            securityToken.AccessToken,
            securityToken.RefreshToken,
            cancellationToken);

        // Assert
        result.Should().BeSuccess();
        result.Value.TokenType.Should().Be(securityToken.TokenType);
        result.Value.AccessToken.Should().NotBeNullOrWhiteSpace()
            .And.NotBe(securityToken.AccessToken);
        result.Value.RefreshToken.Should().Be(securityToken.RefreshToken);
        result.Value.ExpiresIn.Should().Be(securityToken.ExpiresIn);
    }

    [Fact]
    public async Task RefreshTokenAsync_WhenUserDoesNotExist_ShouldReturnInvalidRefreshToken()
    {
        // Arrange
        var securityToken = await sut.GenerateTokenAsync(user, cancellationToken);

        A.CallTo(() => users.FindAsync(new object?[] { user.Id }, cancellationToken))
            .Returns(null);

        // Act
        var result = await sut.RefreshTokenAsync(
            securityToken.AccessToken,
            securityToken.RefreshToken,
            cancellationToken);

        // Assert
        result.Should().BeFailure(SecurityTokenErrors.InvalidRefreshToken);
    }
    
    [Fact]
    public async Task RefreshTokenAsync_WhenRefreshTokenEntityDoesNotExist_ShouldReturnInvalidRefreshToken()
    {
        // Arrange
        var securityToken = await sut.GenerateTokenAsync(user, cancellationToken);

        A.CallTo(() => users.FindAsync(new object?[] { user.Id }, cancellationToken))
            .Returns(user);

        A.CallTo(() => refreshTokens.FindAsync(
                new object?[] { user.Id, securityToken.RefreshToken },
                cancellationToken))
            .Returns(null);

        // Act
        var result = await sut.RefreshTokenAsync(
            securityToken.AccessToken,
            securityToken.RefreshToken,
            cancellationToken);

        // Assert
        result.Should().BeFailure(SecurityTokenErrors.InvalidRefreshToken);
    }

    [Fact]
    public async Task RefreshTokenAsync_WhenAccessTokenIdIsInvalid_ShouldReturnInvalidRefreshToken()
    {
        // Arrange
        var securityToken = await sut.GenerateTokenAsync(user, cancellationToken);

        var refreshToken = RefreshToken.Create(
            user.Id,
            Guid.NewGuid().ToString(),
            DateTimeOffset.UtcNow.AddDays(1));

        A.CallTo(() => users.FindAsync(new object?[] { user.Id }, cancellationToken))
            .Returns(user);

        A.CallTo(() => refreshTokens.FindAsync(
                new object?[] { user.Id, securityToken.RefreshToken },
                cancellationToken))
            .Returns(refreshToken);

        // Act
        var result = await sut.RefreshTokenAsync(
            securityToken.AccessToken,
            securityToken.RefreshToken,
            cancellationToken);

        // Assert
        result.Should().BeFailure(SecurityTokenErrors.InvalidRefreshToken);
    }

    [Fact]
    public async Task RefreshTokenAsync_WhenRefreshTokenIsRevoked_ShouldReturnRefreshTokenRevoked()
    {
        // Arrange
        var securityToken = await sut.GenerateTokenAsync(user, cancellationToken);

        var tokenHandler = new JsonWebTokenHandler();
        var tokenClaims = tokenHandler.ReadJsonWebToken(securityToken.AccessToken);
        var jti = tokenClaims.Claims.Single(claim => claim.Type == JwtRegisteredClaimNames.Jti).Value;

        var refreshToken = RefreshToken.Create(
            user.Id,
            jti,
            DateTimeOffset.UtcNow.AddDays(1));

        refreshToken.MarkAsRevoked(DateTimeOffset.UtcNow);

        A.CallTo(() => users.FindAsync(new object?[] { user.Id }, cancellationToken))
            .Returns(user);

        A.CallTo(() => refreshTokens.FindAsync(
                new object?[] { user.Id, securityToken.RefreshToken },
                cancellationToken))
            .Returns(refreshToken);

        // Act
        var result = await sut.RefreshTokenAsync(
            securityToken.AccessToken,
            securityToken.RefreshToken,
            cancellationToken);

        // Assert
        result.Should().BeFailure(SecurityTokenErrors.RefreshTokenRevoked);
    }
    
    [Fact]
    public async Task RefreshTokenAsync_WhenRefreshTokenHasExpired_ShouldReturnRefreshTokenExpired()
    {
        // Arrange
        var securityToken = await sut.GenerateTokenAsync(user, cancellationToken);

        var tokenHandler = new JsonWebTokenHandler();
        var tokenClaims = tokenHandler.ReadJsonWebToken(securityToken.AccessToken);
        var jti = tokenClaims.Claims.Single(claim => claim.Type == JwtRegisteredClaimNames.Jti).Value;

        var refreshToken = RefreshToken.Create(
            user.Id,
            jti,
            DateTimeOffset.UtcNow.AddMinutes(-1));

        A.CallTo(() => users.FindAsync(new object?[] { user.Id }, cancellationToken))
            .Returns(user);

        A.CallTo(() => refreshTokens.FindAsync(
                new object?[] { user.Id, securityToken.RefreshToken },
                cancellationToken))
            .Returns(refreshToken);

        // Act
        var result = await sut.RefreshTokenAsync(
            securityToken.AccessToken,
            securityToken.RefreshToken,
            cancellationToken);

        // Assert
        result.Should().BeFailure(SecurityTokenErrors.RefreshTokenExpired);
    }
    
    [Fact]
    public async Task RefreshTokenAsync_WhenRefreshTokenIsUsed_ShouldReturnRefreshTokenUsed()
    {
        // Arrange
        var securityToken = await sut.GenerateTokenAsync(user, cancellationToken);

        var tokenHandler = new JsonWebTokenHandler();
        var tokenClaims = tokenHandler.ReadJsonWebToken(securityToken.AccessToken);
        var jti = tokenClaims.Claims.Single(claim => claim.Type == JwtRegisteredClaimNames.Jti).Value;

        var refreshToken = RefreshToken.Create(
            user.Id,
            jti,
            DateTimeOffset.UtcNow.AddDays(1));

        refreshToken.MarkAsUsed(DateTimeOffset.UtcNow);

        A.CallTo(() => users.FindAsync(new object?[] { user.Id }, cancellationToken))
            .Returns(user);

        A.CallTo(() => refreshTokens.FindAsync(
                new object?[] { user.Id, securityToken.RefreshToken },
                cancellationToken))
            .Returns(refreshToken);

        // Act
        var result = await sut.RefreshTokenAsync(
            securityToken.AccessToken,
            securityToken.RefreshToken,
            cancellationToken);

        // Assert
        result.Should().BeFailure(SecurityTokenErrors.RefreshTokenUsed);
    }
}