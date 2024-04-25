using Motorent.Domain.Users;

namespace Motorent.Api.IntegrationTests.Users;

public sealed class RegisterTests(WebApiFactory api) : WebApiFactoryFixture(api)
{
    [Fact]
    public async Task Register_WhenCommandIsValid_ShouldCreateUser()
    {
        // Arrange
        var message = Requests.User.RegisterHttpRequest(Requests.User.RegisterRequest);

        // Act
        var response = await Client.SendAsync(message);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var user = DataContext.Set<User>()
            .AsNoTracking()
            .SingleOrDefault(u => u.Email == Requests.User.RegisterRequest.Email);

        user.Should().NotBeNull();
    }

    [Fact]
    public async Task Register_WhenEmailIsDuplicate_ShouldNotCreateUser()
    {
        // Arrange
        await ResetDatabaseAsync();
        
        await Client.SendAsync(Requests.User.RegisterHttpRequest(Requests.User.RegisterRequest));

        // Act
        var response = await Client.SendAsync(
            Requests.User.RegisterHttpRequest(Requests.User.RegisterRequest));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);

        var users = await DataContext.Set<User>()
            .CountAsync(u => u.Email == Requests.User.RegisterRequest.Email);

        users.Should().Be(1);
    }
}