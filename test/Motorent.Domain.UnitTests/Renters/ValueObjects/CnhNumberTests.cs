using Motorent.Domain.Renters.ValueObjects;

namespace Motorent.Domain.UnitTests.Renters.ValueObjects;

[TestSubject(typeof(CnhNumber))]
public sealed class CnhNumberTests
{
    public static readonly IEnumerable<object[]> ValidCnhNumbers = new List<object[]>
    {
        new object[] { "83907030608" },
        new object[] { "50542712396" },
        new object[] { "84407903668" },
        new object[] { "26775490609" },
        new object[] { "00351664298" }
    };

    public static readonly IEnumerable<object[]> InvalidCnhNumbers = new List<object[]>
    {
        new object[] { null! },
        new object[] { string.Empty },
        new object[] { "           " },
        new object[] { "2677549060" },
        new object[] { "00351864298" },
        new object[] { "11111111111" }
    };

    [Theory, MemberData(nameof(ValidCnhNumbers))]
    public void Create_WhenValueIsAValidCnhNumber_ShouldReturnCnhNumber(string value)
    {
        // Arrange
        // Act
        var result = CnhNumber.Create(value);

        // Assert
        result.Should().BeSuccess(value);
    }
    
    [Theory, MemberData(nameof(InvalidCnhNumbers))]
    public void Create_WhenValueIsAnInvalidCnhNumber_ShouldReturnInvalid(string value)
    {
        // Arrange
        // Act
        var result = CnhNumber.Create(value);

        // Assert
        result.Should().BeFailure(CnhNumber.Invalid);
    }
}