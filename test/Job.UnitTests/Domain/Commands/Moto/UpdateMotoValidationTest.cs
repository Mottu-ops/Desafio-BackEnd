using Job.Commons.Domain.Commands.Moto;
using Job.Domain.Commands.Moto.Validations;

namespace Job.UnitTests.Domain.Commands.Moto;

[Trait("Validation", "UpdateMotoValidation")]
public class UpdateMotoValidationTest
{
    private readonly UpdateMotoValidation _validator = new();

    [Fact]
    public void ShouldReturnErrorWhenCommandIsEmpty()
    {
        // Arrange
        var command = UpdateMotoCommandFaker.Empty().Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
        result.ShouldHaveValidationErrorFor(x => x.Year);
        result.ShouldHaveValidationErrorFor(x => x.Model);
        result.ShouldHaveValidationErrorFor(x => x.Plate);
    }

    [Fact]
    public void ShouldReturnErrorWhenModelIsInvalid()
    {
        // Arrange
        var command = UpdateMotoCommandFaker.Invalid().Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
        result.ShouldHaveValidationErrorFor(x => x.Year);
        result.ShouldHaveValidationErrorFor(x => x.Model);
        result.ShouldHaveValidationErrorFor(x => x.Plate);
    }

    [Fact]
    public void ShouldNotReturnErrorWhenCommandIsValid()
    {
        // Arrange
        var command = UpdateMotoCommandFaker.Default().Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Id);
        result.ShouldNotHaveValidationErrorFor(x => x.Year);
        result.ShouldNotHaveValidationErrorFor(x => x.Model);
        result.ShouldNotHaveValidationErrorFor(x => x.Plate);
    }
}