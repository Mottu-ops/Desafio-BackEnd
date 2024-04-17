using Microsoft.EntityFrameworkCore;
using Motorent.Domain.Users;
using Motorent.Infrastructure.Common.Persistence;
using Motorent.Infrastructure.Users;

namespace Motorent.Infrastructure.UnitTests.Users;

[TestSubject(typeof(EmailUniquenessChecker))]
public sealed class EmailUniquenessCheckerTests
{
    private readonly DataContext dataContext = A.Fake<DataContext>();

    private readonly DbSet<User> users = new List<User>
        {
            Factories.User.CreateUserAsync(email: "john@doe.com").Result.Value
        }
        .BuildMockDbSet();

    private readonly CancellationToken cancellationToken = A.Dummy<CancellationToken>();

    private readonly EmailUniquenessChecker sut;

    public EmailUniquenessCheckerTests()
    {
        sut = new EmailUniquenessChecker(dataContext);

        A.CallTo(() => dataContext.Set<User>())
            .Returns(users);
    }

    [Fact]
    public async Task IsUniqueAsync_WhenEmailIsUnique_ShouldReturnTrue()
    {
        // Arrange
        const string email = "jane@doe.com";

        // Act
        var result = await sut.IsUniqueAsync(email, cancellationToken);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task IsUniqueAsync_WhenEmailIsNotUnique_ShouldReturnFalse()
    {
        // Arrange
        const string email = "john@doe.com";

        // Act
        var result = await sut.IsUniqueAsync(email, cancellationToken);

        // Assert
        result.Should().BeFalse();
    }
}