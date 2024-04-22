using Motorent.Domain.Renters.ValueObjects;

namespace Motorent.Domain.UnitTests.Renters.ValueObjects;

[TestSubject(typeof(Cnpj))]
public sealed class CnpjTests
{
    public static readonly IEnumerable<object[]> ValidCnpjs = new List<object[]>
    {
        new object[] { "27137033000134" },
        new object[] { "92.443.739/0001-96" },
        new object[] { "97877245000133" },
        new object[] { "96.657.097/0001-89" },
        new object[] { "72.302.462/0001-74" }
    };

    public static readonly IEnumerable<object[]> InvalidCnpjs = new List<object[]>
    {
        new object[] { null! },
        new object[] { string.Empty },
        new object[] { "           " },
        new object[] { "2713703300013" },
        new object[] { "96.657.09780001-89" },
        new object[] { "74798886000161" }
    };

    [Theory, MemberData(nameof(ValidCnpjs))]
    public void Create_WhenValueIsAValidCnpj_ShouldReturnCnpj(string value)
    {
        // Arrange
        // Act
        var result = Cnpj.Create(value);

        // Assert
        result.Should().BeSuccess(value);
    }
    
    [Theory, MemberData(nameof(InvalidCnpjs))]
    public void Create_WhenValueIsAnInvalidCnpj_ShouldReturnInvalid(string value)
    {
        // Arrange
        // Act
        var result = Cnpj.Create(value);

        // Assert
        result.Should().BeFailure(Cnpj.Invalid);
    }
}