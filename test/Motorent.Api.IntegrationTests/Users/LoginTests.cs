using Microsoft.IdentityModel.JsonWebTokens;
using Motorent.Contracts.Users.Responses;
using Motorent.Domain.Users;
using Motorent.Domain.Users.ValueObjects;
using Motorent.Infrastructure.Common.Identity;

namespace Motorent.Api.IntegrationTests.Users;

public sealed class LoginTests(WebAppFactory app) : IntegrationTest(app)
{
    [Fact]
    public async Task Login_WhenUserExistsAndPasswordIsCorrect_ShouldReturnValidTokenResponse()
    {
        // Arrange
        await Client.SendAsync(Requests.User.CreateHttpRegisterRequest(Requests.User.RegisterRequest));

        var message = Requests.User.CreateHttpLoginRequest(Requests.User.LoginRequest);

        // Act
        var response = await Client.SendAsync(message);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>(
            ApiSerializationOptions.Options);

        tokenResponse.Should().NotBeNull();
        tokenResponse!.TokenType.Should().Be("Bearer");
        tokenResponse.AccessToken.Should().NotBeNullOrWhiteSpace();
        tokenResponse.RefreshToken.Should().NotBeNullOrWhiteSpace();
        tokenResponse.ExpiresIn.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Login_WhenUserExistsAndPasswordIsCorrect_ShouldReturnTokenResponse()
    {
        // Arrange
        await ResetDatabaseAsync();

        await Client.SendAsync(Requests.User.CreateHttpRegisterRequest(Requests.User.RegisterRequest));

        var message = Requests.User.CreateHttpLoginRequest(Requests.User.LoginRequest);

        // Act
        var response = await Client.SendAsync(message);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>(
            ApiSerializationOptions.Options);

        tokenResponse.Should().NotBeNull();

        var securityTokenHandler = new JsonWebTokenHandler();
        var accessToken = securityTokenHandler.ReadJsonWebToken(tokenResponse!.AccessToken);

        var sub = accessToken.Claims.SingleOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var role = accessToken.Claims.SingleOrDefault(c => c.Type == ClaimsPrincipalExtensions.RoleClaimType)?.Value;
        var birthdate = accessToken.Claims.SingleOrDefault(c => c.Type == JwtRegisteredClaimNames.Birthdate)?.Value;

        sub.Should().NotBeNull();

        var user = await DataContext.Set<User>()
            .FindAsync([new UserId(Ulid.Parse(sub!))]);

        user.Should().NotBeNull();
        role.Should().Be(user!.Role.Name);
        birthdate.Should().Be(user.Birthdate.Value.ToString("yyyy-MM-dd"));
    }

    [Fact]
    public async Task Login_WhenUserDoesNotExist_ShouldReturnUnauthorized()
    {
        // Arrange
        await ResetDatabaseAsync();

        var message = Requests.User.CreateHttpLoginRequest(Requests.User.LoginRequest);

        // Act
        var response = await Client.SendAsync(message);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var exists = await DataContext.Set<User>()
            .AnyAsync(u => u.Email == Requests.User.LoginRequest.Email);

        exists.Should().BeFalse();
    }

    [Fact]
    public async Task Login_WhenUserExistsButPasswordIsIncorrect_ShouldReturnUnauthorized()
    {
        // Arrange
        await Client.SendAsync(Requests.User.CreateHttpRegisterRequest(Requests.User.RegisterRequest));

        var message = Requests.User.CreateHttpLoginRequest(Requests.User.LoginRequest with
        {
            Password = "quero um emprego :("
        });

        // Act
        var response = await Client.SendAsync(message);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var exists = await DataContext.Set<User>()
            .AnyAsync(u => u.Email == Requests.User.LoginRequest.Email);

        exists.Should().BeTrue();
    }
}