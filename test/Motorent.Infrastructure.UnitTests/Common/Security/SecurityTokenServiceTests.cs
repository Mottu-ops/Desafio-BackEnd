using System.IdentityModel.Tokens.Jwt;
using FakeItEasy;
using FluentAssertions;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Motorent.Infrastructure.Common.Security;

namespace Motorent.Infrastructure.UnitTests.Common.Security;

[TestSubject(typeof(SecurityTokenService))]
public sealed class SecurityTokenServiceTests
{
    private readonly TimeProvider timeProvider = A.Fake<TimeProvider>(fakeOptions =>
        fakeOptions.Wrapping(TimeProvider.System));

    private readonly IOptions<SecurityTokenOptions> options = Options.Create(new SecurityTokenOptions
    {
        Key = "PePkSCXr0OMSgKCN06sB8sIRMXhhWQpB",
        Issuer = "localhost",
        Audience = "localhost",
        ExpiresInMinutes = 5
    });

    private readonly SecurityTokenService sut;

    public SecurityTokenServiceTests()
    {
        sut = new SecurityTokenService(timeProvider, options);
    }

    [Fact]
    public async Task SecurityTokenService_WhenCalled_ShouldGenerateToken()
    {
        // Act
        var result = await sut.GenerateTokenAsync();

        // Assert
        result.Should().NotBeNull();
        result.AccessToken.Should().NotBeNullOrWhiteSpace();
        result.ExpiresIn.Should().Be(options.Value.ExpiresInMinutes);
    }

    [Fact]
    public async Task SecurityTokenService_WhenCalled_ShouldGenerateTokenWithCorrectInformation()
    {
        // Act
        var result = await sut.GenerateTokenAsync();
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

        token.Claims.Should().HaveCount(5);
        token.Claims.Should().ContainSingle(claim => claim.Type == JwtRegisteredClaimNames.Jti);
        token.Claims.Single(claim => claim.Type == JwtRegisteredClaimNames.Jti).Value.Should()
            .NotBeNullOrWhiteSpace();
    }
}