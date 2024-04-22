using Motorent.Domain.Renters.Enums;
using Motorent.Domain.Renters.ValueObjects;

namespace Motorent.Domain.UnitTests.Renters.ValueObjects;

[TestSubject(typeof(Cnh))]
public sealed class CnhTests
{
    [Fact]
    public void Create_WhenIsExpired_ShouldReturnExpired()
    {
        // Arrange
        var number = CnhNumber.Create("47637911411").Value;
        var category = CnhCategory.AB;
        var expirationDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-1));
        
        // Act
        var result = Cnh.Create(number, expirationDate, category);
        
        // Assert
        result.Should().BeFailure(Cnh.Expired);
    }

    [Fact]
    public void Create_WhenIsNotExpired_ShouldReturnCnh()
    {
        // Arrange
        var number = CnhNumber.Create("47637911411").Value;
        var category = CnhCategory.AB;
        var expirationDate = DateOnly.FromDateTime(DateTime.Today.AddYears(1));

        // Act
        var result = Cnh.Create(number, expirationDate, category);

        // Assert
        result.Should().BeSuccess()
            .Which.Value.Should().BeEquivalentTo(new
            {
                Number = number,
                Category = category,
                ExpirationDate = expirationDate
            });
    }
}