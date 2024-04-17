using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Motorent.Infrastructure.Common.Identity;

namespace Motorent.Infrastructure.UnitTests.Common.Identity;

[TestSubject(typeof(UserContext))]
public sealed class UserContextTests
{
    private const string UserId = "91555802-d6cf-4857-8413-9e9f69d0fadc";
    private const string Role = "admin";

    private readonly ClaimsPrincipal authenticatedUser = new(new ClaimsIdentity(
    [
        new Claim(ClaimTypes.NameIdentifier, UserId),
        new Claim(ClaimTypes.Role, Role)
    ], "authenticated"));

    private readonly ClaimsPrincipal unauthorizedUser = new(new ClaimsIdentity());

    private readonly UserContext userContextAuthenticated;
    private readonly UserContext userContextUnauthenticated;

    public UserContextTests()
    {
        var httpContextAccessor1 = A.Fake<IHttpContextAccessor>();
        var httpContextAccessor2 = A.Fake<IHttpContextAccessor>();

        A.CallTo(() => httpContextAccessor1.HttpContext!.User)
            .Returns(authenticatedUser);

        A.CallTo(() => httpContextAccessor2.HttpContext!.User)
            .Returns(unauthorizedUser);

        userContextAuthenticated = new UserContext(httpContextAccessor1);
        userContextUnauthenticated = new UserContext(httpContextAccessor2);
    }

    [Fact]
    public void IsAuthenticated_WhenUserIsAuthenticated_ShouldReturnTrue()
    {
        // Arrange
        // Act
        var isAuthenticated = userContextAuthenticated.IsAuthenticated;

        // Assert
        isAuthenticated.Should().BeTrue();
    }

    [Fact]
    public void IsAuthenticated_WhenUserIsNotAuthenticated_ShouldReturnFalse()
    {
        // Arrange
        // Act
        var isAuthenticated = userContextUnauthenticated.IsAuthenticated;

        // Assert
        isAuthenticated.Should().BeFalse();
    }

    [Fact]
    public void UserId_WhenUserIsAuthenticated_ShouldReturnUserId()
    {
        // Arrange
        // Act
        var userId = userContextAuthenticated.UserId;

        // Assert
        userId.Value.Should().Be(UserId);
    }

    [Fact]
    public void UserId_WhenUserIsNotAuthenticated_ShouldReturnEmptyUserId()
    {
        // Arrange
        // Act
        var userId = userContextUnauthenticated.UserId;

        // Assert
        userId.Should().NotBe(UserId);
    }

    [Fact]
    public void Role_WhenUserIsAuthenticated_ShouldReturnRole()
    {
        // Arrange
        // Act
        var role = userContextAuthenticated.Role;

        // Assert
        role.Should().Be(Domain.Users.Enums.Role.FromName(Role));
    }

    [Fact]
    public void Role_WhenUserIsNotAuthenticated_ShouldReturnNoneRole()
    {
        // Arrange
        // Act
        var role = userContextUnauthenticated.Role;

        // Assert
        role.Should().Be(Domain.Users.Enums.Role.None);
    }
}