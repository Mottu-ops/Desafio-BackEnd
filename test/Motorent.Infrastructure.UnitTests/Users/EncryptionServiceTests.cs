using FluentAssertions;
using JetBrains.Annotations;
using Motorent.Infrastructure.Users;

namespace Motorent.Infrastructure.UnitTests.Users;

[TestSubject(typeof(EncryptionService))]
public sealed class EncryptionServiceTests
{
    private readonly EncryptionService sut = new();
    
    [Fact]
    public void Encrypt_WhenCalled_ShouldReturnEncryptedValueInCorrectFormat()
    {
        // Arrange
        const string password = "password";
        
        // Act
        var result = sut.Encrypt(password);
        
        // Assert
        var parts = result.Split(':');

        parts.Length.Should().Be(4);
    }
    
    [Fact]
    public void Encrypt_WhenCalledMultipleTimes_ShouldReturnDifferentValues()
    {
        // Arrange
        const string password = "password";
        
        // Act
        var result1 = sut.Encrypt(password);
        var result2 = sut.Encrypt(password);
        
        // Assert
        result1.Should().NotBe(result2);
    }
    
    [Fact]
    public void Verify_WhenCalledWithCorrectPassword_ShouldReturnTrue()
    {
        // Arrange
        const string password = "password";
        var hash = sut.Encrypt(password);
        
        // Act
        var result = sut.Verify(password, hash);
        
        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void Verify_WhenCalledWithIncorrectPassword_ShouldReturnFalse()
    {
        // Arrange
        const string password = "password";
        var hash = sut.Encrypt(password);
        
        // Act
        var result = sut.Verify("wrong password", hash);
        
        // Assert
        result.Should().BeFalse();
    }
}