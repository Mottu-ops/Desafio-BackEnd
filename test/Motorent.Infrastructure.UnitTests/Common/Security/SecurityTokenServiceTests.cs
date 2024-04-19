using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Motorent.Domain.Users;
using Motorent.Infrastructure.Common.Identity;
using Motorent.Infrastructure.Common.Persistence;
using Motorent.Infrastructure.Common.Security;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Motorent.Infrastructure.UnitTests.Common.Security;

[TestSubject(typeof(SecurityTokenService))]
public sealed class SecurityTokenServiceTests
{
    private readonly DataContext dataContext = A.Fake<DataContext>();

    private readonly DbSet<RefreshToken> refreshTokens = new List<RefreshToken>()
        .BuildMockDbSet();

    private readonly TimeProvider timeProvider = A.Fake<TimeProvider>(fakeOptions =>
        fakeOptions.Wrapping(TimeProvider.System));
    
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

        A.CallTo(() => dataContext.Set<RefreshToken>())
            .Returns(refreshTokens);
    }

    [Fact]
    public async Task GenerateTokenAsync_WhenCalled_ShouldReturnSecurityToken()
    {
        // Act
        var result = await sut.GenerateTokenAsync(user);

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
        var result = await sut.GenerateTokenAsync(user);
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(result.AccessToken);

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
        var handler = new JwtSecurityTokenHandler();

        // Act
        var result = await sut.GenerateTokenAsync(user);

        // Assert
        var token = handler.ReadJwtToken(result.AccessToken);

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
}