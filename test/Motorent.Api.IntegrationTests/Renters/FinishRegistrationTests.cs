using Motorent.Domain.Renters;

namespace Motorent.Api.IntegrationTests.Renters;

public sealed class FinishRegistrationTests(WebApiFactory api) : WebApiFactoryFixture(api)
{
    [Fact]
    public async Task FinishRegistration_WhenUserIsNotAuthenticated_ShouldReturnUnauthorized()
    {
        // Arrange
        var message = Requests.Renter.FinishRegistrationHttpRequest(
            Requests.Renter.FinishRegistrationRequest);

        // Act
        var response = await Client.SendAsync(message);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var count = await DataContext.Set<Renter>()
            .CountAsync();
        
        count.Should().Be(0);
    }

    [Fact]
    public async Task FinishRegistration_WhenUserIsAuthenticatedAndHasNotFinishedRegistration_ShouldReturnOk()
    {
        // Arrange
        var userId = await CreateUserAsync(UserData.Default with
        {
            CreateRenter = false
        });

        await AuthenticateAsync(userId);

        var message = Requests.Renter.FinishRegistrationHttpRequest(
            Requests.Renter.FinishRegistrationRequest);

        // Act
        var response = await Client.SendAsync(message);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var renter = await DataContext.Set<Renter>()
            .SingleOrDefaultAsync(r => r.UserId == userId);
        
        renter.Should().NotBeNull();
    }

    [Fact]
    public async Task FinishRegistration_WhenUserIsAuthenticatedAndHasFinishedRegistration_ShouldReturnForbidden()
    {
        // Arrange
        var userId = await CreateUserAsync(UserData.Default);

        await AuthenticateAsync(userId);

        var message = Requests.Renter.FinishRegistrationHttpRequest(
            Requests.Renter.FinishRegistrationRequest);

        // Act
        var response = await Client.SendAsync(message);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);

        var count = await DataContext.Set<Renter>()
            .CountAsync(r => r.UserId == userId);
        
        count.Should().Be(1);
    }
}