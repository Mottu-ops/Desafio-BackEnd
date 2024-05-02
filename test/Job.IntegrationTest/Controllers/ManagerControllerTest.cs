using System.Net;
using System.Net.Http.Json;
using Bogus;
using FluentAssertions;
using Job.Domain.Commands.User.Manager;

namespace Job.IntegrationTest.Controllers;

[Collection("Database")]
public class ManagerControllerTest(SetupFactory factory) : IClassFixture<SetupFactory>
{
    private readonly Faker _faker = new Faker();

    [Fact]
    public async Task AuthenticationManagerCommand_WhenValid_ShouldReturnUnauthorized()
    {
        // Arrange
        var client = factory.CreateClient();
        var content = new AuthenticationManagerCommand(_faker.Person.Email, _faker.Internet.Password());

        // Act
        var response = await client.PostAsJsonAsync("/manager/authentication", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task AuthenticationManagerCommand_WhenValid_ShouldReturnOk()
    {
        // Arrange
        var client = factory.CreateClient();
        var content = new AuthenticationManagerCommand("job@job.com", "mudar@123");

        // Act
        var response = await client.PostAsJsonAsync("/manager/authentication", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task AuthenticationManagerCommand_WhenValid_ShouldReturnBadRequest()
    {
        // Arrange
        var client = factory.CreateClient();
        var content = new AuthenticationManagerCommand(_faker.Random.AlphaNumeric(10), _faker.Random.AlphaNumeric(5));

        // Act
        var response = await client.PostAsJsonAsync("/manager/authentication", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}