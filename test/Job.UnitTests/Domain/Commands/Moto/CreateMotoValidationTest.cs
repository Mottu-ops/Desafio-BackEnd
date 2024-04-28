using Job.Commons.Domain.Commands.Moto;
using Job.Domain.Commands.Moto.Validations;

namespace Job.UnitTests.Domain.Commands.Moto;

[Trait("Validation", "CreateMotoValidation")]
public class CreateMotoValidationTest
{
    private readonly CreateMotoValidation _validator = new();

    [Fact]
    public void ShouldReturnErrorWhenCommandIsEmpty()
    {
        // Arrange
        var command = CreateMotoCommandFaker.Empty().Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Year);
        result.ShouldHaveValidationErrorFor(x => x.Model);
        result.ShouldHaveValidationErrorFor(x => x.Plate);
    }

    [Fact]
    public void ShouldReturnErrorWhenModelIsInvalid()
    {
        // Arrange
        var command = CreateMotoCommandFaker.Invalid().Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Year);
        result.ShouldHaveValidationErrorFor(x => x.Model);
        result.ShouldHaveValidationErrorFor(x => x.Plate);
    }

    [Fact]
    public void ShouldNotReturnErrorWhenCommandIsValid()
    {
        // Arrange
        var command = CreateMotoCommandFaker.Default().Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Year);
        result.ShouldNotHaveValidationErrorFor(x => x.Model);
        result.ShouldNotHaveValidationErrorFor(x => x.Plate);
    }

}