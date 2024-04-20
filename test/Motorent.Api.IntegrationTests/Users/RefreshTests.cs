using Motorent.Contracts.Users.Requests;
using Motorent.Contracts.Users.Responses;
using Motorent.Infrastructure.Common.Security;

namespace Motorent.Api.IntegrationTests.Users;

public sealed class RefreshTests(WebAppFactory app) : IntegrationTest(app)
{
    [Fact]
    public async Task RefreshToken_WhenTokenIsValid_ShouldReturnValidTokenResponse()
    {
        // Arrange
        await Client.SendAsync(Requests.User.CreateHttpRegisterRequest(Requests.User.RegisterRequest));
        var loginResponse = await Client.SendAsync(
            Requests.User.CreateHttpLoginRequest(Requests.User.LoginRequest));

        var tokenResponse = await loginResponse.Content.ReadFromJsonAsync<TokenResponse>(
            ApiSerializationOptions.Options);

        var message = Requests.User.CreateHttpRefreshTokenRequest(new RefreshTokenRequest
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
        await Client.SendAsync(Requests.User.CreateHttpRegisterRequest(Requests.User.RegisterRequest));
        var loginResponse = await Client.SendAsync(
            Requests.User.CreateHttpLoginRequest(Requests.User.LoginRequest));

        var tokenResponse = await loginResponse.Content.ReadFromJsonAsync<TokenResponse>(
            ApiSerializationOptions.Options);

        var message = Requests.User.CreateHttpRefreshTokenRequest(new RefreshTokenRequest
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
        await Client.SendAsync(Requests.User.CreateHttpRegisterRequest(Requests.User.RegisterRequest));
        var loginResponse = await Client.SendAsync(
            Requests.User.CreateHttpLoginRequest(Requests.User.LoginRequest));

        var tokenResponse = await loginResponse.Content.ReadFromJsonAsync<TokenResponse>(
            ApiSerializationOptions.Options);

        var message = Requests.User.CreateHttpRefreshTokenRequest(new RefreshTokenRequest
        {
            AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI5MDM2NzIzZS0xMTFhLTQ3NjItOWIzMC1iOWNkMzc" +
                          "zN2VmYTMiLCJzdWIiOiIwZjUzNmJiZS1jZjFkLTQ1MjgtYTYxMy1jODg0YWYyZjc0NmUiLCJyb2xlIjoicmVudGVy" +
                          "IiwibmFtZSI6IkpvaG4gRG9lIiwiYmlydGhkYXRlIjoiMjAwMC0wMS0wMSIsImF1ZCI6ImxvY2FsaG9zdCIsImlz" +
                          "cyI6ImxvY2FsaG9zdCIsImV4cCI6MTcxMzU3Mzc2NywibmJmIjoxNzEzNTczNzA3fQ.wluhLsKmgWRcfNUEbMFTMh" +
                          "AyRxQ5g3Pxj9e5q-kLUfE",
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
        await Client.SendAsync(Requests.User.CreateHttpRegisterRequest(Requests.User.RegisterRequest));
        var loginResponse = await Client.SendAsync(
            Requests.User.CreateHttpLoginRequest(Requests.User.LoginRequest));

        var tokenResponse = await loginResponse.Content.ReadFromJsonAsync<TokenResponse>(
            ApiSerializationOptions.Options);

        var message = Requests.User.CreateHttpRefreshTokenRequest(new RefreshTokenRequest
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