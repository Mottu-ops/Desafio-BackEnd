using Job.Commons.Domain.Commands.Rent;
using Job.Domain.Commands.Rent.Validations;

namespace Job.UnitTests.Domain.Commands.Rental;

[Trait("Validation", "CreateRentalValidation")]
public class CreateRentalValidationTest
{
    private readonly CreateRentalValidation _validator = new();

    [Fact]
    public void ShouldReturnErrorWhenIdMotoIsEmpty()
    {
        // Arrange
        var command = CreateRentCommandFaker.Empty().Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.IdMoto);
    }

    [Fact]
    public void ShouldReturnErrorWhenDatePreviewIsInvalid()
    {
        // Arrange
        var command = CreateRentCommandFaker.Invalid().Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.DatePreview);
    }

    [Fact]
    public void ShouldReturnErrorWhenPlanIsInvalid()
    {
        // Arrange
        var command = CreateRentCommandFaker.Invalid().Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Plan);
    }

    [Fact]
    public void ShouldNotReturnErrorWhenCommandIsValid()
    {
        // Arrange
        var command = CreateRentCommandFaker.Default().Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.IdMoto);
        result.ShouldNotHaveValidationErrorFor(x => x.DatePreview);
        result.ShouldNotHaveValidationErrorFor(x => x.Plan);
    }
    
}