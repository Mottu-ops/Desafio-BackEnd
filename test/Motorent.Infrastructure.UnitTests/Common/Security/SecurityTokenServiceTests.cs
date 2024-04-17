using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Motorent.Domain.Users;
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

    private readonly User user = Factories.User.CreateUserAsync()
        .Result
        .Value;

    private readonly SecurityTokenService sut;

    public SecurityTokenServiceTests()
    {
        sut = new SecurityTokenService(timeProvider, options);
    }

    [Fact]
    public async Task SecurityTokenService_WhenCalled_ShouldGenerateToken()
    {
        // Act
        var result = await sut.GenerateTokenAsync(user);

        // Assert
        result.Should().NotBeNull();
        result.AccessToken.Should().NotBeNullOrWhiteSpace();
        result.ExpiresIn.Should().Be(options.Value.ExpiresInMinutes);
    }

    [Fact]
    public async Task SecurityTokenService_WhenCalled_ShouldGenerateTokenWithCorrectInformation()
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
        
        token.Claims.Should().HaveCount(8);
        token.Claims.Should().ContainSingle(claim =>
            claim.Type == JwtRegisteredClaimNames.Jti &&
            claim.ValueType == ClaimValueTypes.String &&
            claim.Value.Length == 36);
        
        token.Claims.Should().ContainSingle(claim =>
            claim.Type == JwtRegisteredClaimNames.Sub &&
            claim.ValueType == ClaimValueTypes.String &&
            claim.Value == user.Id.ToString());
        
        token.Claims.Should().ContainSingle(claim =>
            claim.Type == JwtRegisteredClaimNames.Name &&
            claim.ValueType == ClaimValueTypes.String &&
            claim.Value == user.Name);
        
        token.Claims.Should().ContainSingle(claim =>
            claim.Type == JwtRegisteredClaimNames.Birthdate &&
            claim.ValueType == ClaimValueTypes.String &&
            claim.Value == user.Birthdate.ToString("yyyy-MM-dd"));
    }
}