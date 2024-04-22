using Motorent.Domain.Common.ValueObjects;

namespace Motorent.Domain.UnitTests.Common.ValueObjects;

[TestSubject(typeof(Birthdate))]
public class BirthdateTests
{
    [Fact]
    public void Create_WhenAgeIsOver18_ShouldReturnBirthdate()
    {
        // Arrange
        var date = new DateOnly(2000, 1, 1);

        // Act  
        var result = Birthdate.Create(date);

        // Assert
        result.Should().BeSuccess()
            .Which.Value.Value.Should().Be(date);
    }

    [Fact]
    public void Create_WhenAgeIsUnder18_ShouldReturnMustBeAtLeast18YearsOld()
    {
        // Arrange
        var date = new DateOnly(2020, 1, 1);

        // Act  
        var result = Birthdate.Create(date);

        // Assert
        result.Should().BeFailure(Birthdate.MustBeAtLeast18YearsOld);
    }

    [Fact]
    public void Create_WhenAgeIsUnder18ByOneDay_ShouldReturnMustBeAtLeast18YearsOld()
    {
        // Arrange
        var today = DateTime.Today;
        var year = today.Year - 18;
        var month = today.Month;
        var day = today.Day;

        // Se hoje é o último dia do mês, então inicia-se o próximo mês
        if (day + 1 > DateTime.DaysInMonth(year, month))
        {
            month++;
            day = 1;
        }
        else
        {
            day++;
        }

        var date = new DateOnly(year, month, day);

        // Act
        var result = Birthdate.Create(date);

        // Assert
        result.Should().BeFailure(Birthdate.MustBeAtLeast18YearsOld);
    }
}