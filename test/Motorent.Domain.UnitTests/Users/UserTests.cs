using FakeItEasy;
using FluentAssertions;
using JetBrains.Annotations;
using Motorent.Domain.Users;
using Motorent.Domain.Users.Errors;
using Motorent.Domain.Users.Services;
using Motorent.Domain.Users.ValueObjects;
using Motorent.TestUtils.Constants;
using Motorent.TestUtils.Factories;
using ResultExtensions.FluentAssertions;

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
    public async Task CreateAsync_WhenCalled_ShouldReturnUser()
    {
        // Arrange
        var id = UserId.New();
        const string name = Constants.User.Name;
        const string email = Constants.User.Email;
        const string password = Constants.User.Password;
        const string passwordHash = "hashed-password";
        var birthdate = Constants.User.Birthdate;
        
        A.CallTo(() => emailUniquenessChecker.IsUniqueAsync(email, cancellationToken))
            .Returns(true);
        
        A.CallTo(() => encryptionService.Encrypt(password))
            .Returns(passwordHash);
        
        // Act
        var user = await Factories.User.CreateUserAsync(
            id: id,
            name: name,
            email: email,
            password: password,
            birthdate: birthdate,
            encryptionService: encryptionService,
            emailUniquenessChecker: emailUniquenessChecker);
        
        // Assert
        user.Should().BeSuccess();
        user.Value.Id.Should().Be(id);
        user.Value.Name.Should().Be(name);
        user.Value.Email.Should().Be(email);
        user.Value.PasswordHash.Should().Be(passwordHash);
        user.Value.Birthdate.Should().Be(birthdate);
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