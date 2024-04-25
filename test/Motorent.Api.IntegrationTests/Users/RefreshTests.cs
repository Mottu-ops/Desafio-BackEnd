using Motorent.Contracts.Users.Requests;
using Motorent.Contracts.Users.Responses;
using Motorent.Infrastructure.Common.Security;

namespace Motorent.Api.IntegrationTests.Users;

public sealed class RefreshTests(WebApiFactory api) : WebApiFactoryFixture(api)
{
    [Fact]
    public async Task RefreshToken_WhenTokensArValid_ShouldReturnTokenResponse()
    {
        // Arrange
        await Client.SendAsync(Requests.User.RegisterHttpRequest(Requests.User.RegisterRequest));
        var loginResponse = await Client.SendAsync(
            Requests.User.LoginHttpRequest(Requests.User.LoginRequest));

        var tokenResponse = await loginResponse.Content.ReadFromJsonAsync<TokenResponse>(
            ApiSerializationOptions.Options);

        var message = Requests.User.RefreshTokenHttpRequest(new RefreshTokenRequest
        {
            AccessToken = tokenResponse!.AccessToken,
            RefreshToken = tokenResponse.RefreshToken
        });

        // Act
        var response = await Client.SendAsync(message);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var refreshedTokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>(
            ApiSerializationOptions.Options);

        refreshedTokenResponse.Should().NotBeNull();
        refreshedTokenResponse!.TokenType.Should().Be("Bearer");
        refreshedTokenResponse.AccessToken.Should().NotBeNullOrWhiteSpace()
            .Should().NotBeSameAs(tokenResponse.AccessToken);
        refreshedTokenResponse.RefreshToken.Should().Be(tokenResponse.RefreshToken);
        refreshedTokenResponse.ExpiresIn.Should().Be(tokenResponse.ExpiresIn);
    }

    [Fact]
    public async Task RefreshToken_WhenTokenIsRefreshed_ShouldMarkRefreshTokenAsUsed()
    {
        // Arrange
        await Client.SendAsync(Requests.User.RegisterHttpRequest(Requests.User.RegisterRequest));
        var loginResponse = await Client.SendAsync(
            Requests.User.LoginHttpRequest(Requests.User.LoginRequest));

        var tokenResponse = await loginResponse.Content.ReadFromJsonAsync<TokenResponse>(
            ApiSerializationOptions.Options);

        var message = Requests.User.RefreshTokenHttpRequest(new RefreshTokenRequest
        {
            AccessToken = tokenResponse!.AccessToken,
            RefreshToken = tokenResponse.RefreshToken
        });

        // Act
        var response = await Client.SendAsync(message);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var refreshedTokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>(
            ApiSerializationOptions.Options);

        refreshedTokenResponse.Should().NotBeNull();

        var refreshToken = await DataContext.Set<RefreshToken>()
            .FirstOrDefaultAsync(tr => tr.Token == tokenResponse.RefreshToken);

        refreshToken.Should().NotBeNull();
        refreshToken!.IsUsed.Should().BeTrue();
    }
    
    [Fact]
    public async Task RefreshToken_WhenAccessTokenIsInvalid_ShouldReturnUnauthorized()
    {
        // Arrange
        await Client.SendAsync(Requests.User.RegisterHttpRequest(Requests.User.RegisterRequest));
        var loginResponse = await Client.SendAsync(
            Requests.User.LoginHttpRequest(Requests.User.LoginRequest));

        var tokenResponse = await loginResponse.Content.ReadFromJsonAsync<TokenResponse>(
            ApiSerializationOptions.Options);

        var message = Requests.User.RefreshTokenHttpRequest(new RefreshTokenRequest
        {
            AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI4ODQ2NTRhZS03NDVmLTQ0ZGQtOGIwNC05ZjMyZmU4" +
                          "YTM4MDQiLCJzdWIiOiIwMUhXN1dNUFM3N1RIRTFLOVdaUUFFNzBBUCIsInJvbGUiOiJyZW50ZXIiLCJuYW1lIjoiSm" +
                          "9obiBEb2UiLCJiaXJ0aGRhdGUiOiIyMDAwLTAxLTAxIiwiYXVkIjoibG9jYWxob3N0IiwiaXNzIjoibG9jYWxob3N" +
                          "0IiwiZXhwIjoxNzEzOTU3MjU3LCJuYmYiOjE3MTM5NTY5NTd9.0EsA8NTo4oTnU5nfeD7VaKlBc8Co13GFES9T4fv" +
                          "lZWw",
            RefreshToken = tokenResponse!.RefreshToken
        });

        // Act
        var response = await Client.SendAsync(message);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task RefreshToken_WhenRefreshTokenIsInvalid_ShouldReturnUnauthorized()
    {
        // Arrange
        await Client.SendAsync(Requests.User.RegisterHttpRequest(Requests.User.RegisterRequest));
        var loginResponse = await Client.SendAsync(
            Requests.User.LoginHttpRequest(Requests.User.LoginRequest));

        var tokenResponse = await loginResponse.Content.ReadFromJsonAsync<TokenResponse>(
            ApiSerializationOptions.Options);

        var message = Requests.User.RefreshTokenHttpRequest(new RefreshTokenRequest
        {
            AccessToken = tokenResponse!.AccessToken,
            RefreshToken = "RimGKeC1Uvt4k29PpRiuR/ER4YgLTUDdHLjo1zeeeINGjBcOXESOOcCcMdsg+s0MDErnN6lWLByMRWPMVqtU2g=="
        });

        // Act
        var response = await Client.SendAsync(message);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}