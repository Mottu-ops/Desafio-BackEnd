namespace Motorent.Api.IntegrationTests.Users;

public sealed class GetUserTests(WebApiFactory api) : WebApiFactoryFixture(api)
{
    [Fact]
    public async Task GetUser_WhenUserIsAuthenticated_ShouldReturnOkResponse()
    {
        // Arrange
        var userId = await CreateUserAsync(UserData.Default);
        await AuthenticateAsync(userId);

        var message = Requests.User.GetUserHttpRequest();

        // Act
        var response = await Client.SendAsync(message);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task GetUser_WhenUserIsNotAuthenticated_ShouldReturnUnauthorizedResponse()
    {
        // Arrange
        var message = Requests.User.GetUserHttpRequest();

        // Act
        var response = await Client.SendAsync(message);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}