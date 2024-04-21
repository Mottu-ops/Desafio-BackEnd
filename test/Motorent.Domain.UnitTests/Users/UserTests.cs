using Motorent.Domain.Users;
using Motorent.Domain.Users.Errors;
using Motorent.Domain.Users.Services;

namespace Motorent.Domain.UnitTests.Users;

[TestSubject(typeof(User))]
public sealed class UserTests
{
    private readonly IEncryptionService encryptionService = A.Fake<IEncryptionService>();
    private readonly IEmailUniquenessChecker emailUniquenessChecker = A.Fake<IEmailUniquenessChecker>();
    private readonly CancellationToken cancellationToken = A.Dummy<CancellationToken>();

    public UserTests()
    {
        A.CallTo(() => emailUniquenessChecker.IsUniqueAsync(A<string>._, cancellationToken))
            .Returns(false);
    }
    
    [Fact]
    public async Task CreateAsync_WhenEmailIsNotUnique_ShouldReturnDuplicateEmail()
    {
        // Arrange
        const string email = Constants.User.Email;

        A.CallTo(() => emailUniquenessChecker.IsUniqueAsync(email, cancellationToken))
            .Returns(false);

        // Act
        var result = await Factories.User.CreateUserAsync(
            email: email,
            emailUniquenessChecker: emailUniquenessChecker);

        // Assert
        result.Should().BeFailure(UserErrors.DuplicateEmail(email));
    }

    [Fact]
    public async Task CreateAsync_WhenCalled_ShouldEncryptPassword()
    {
        // Arrange
        const string password = Constants.User.Password;
        const string passwordHash = "hashed-password";
        
        A.CallTo(() => encryptionService.Encrypt(password))
            .Returns(passwordHash);
        
        // Act
        var user = await Factories.User.CreateUserAsync(
            password: password,
            encryptionService: encryptionService);
        
        // Assert
        user.Value.PasswordHash.Should().Be(passwordHash);
    }
}