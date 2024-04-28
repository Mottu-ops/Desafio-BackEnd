using Job.Commons.Domain.Commands.User.Motoboy;
using Job.Domain.Commands.User.Motoboy.Validations;

namespace Job.UnitTests.Domain.Commands.User.Motoboy;

[Trait("Validation", "CreateMotoboyValidation")]
public class CreateMotoboyValidationTest
{
    private readonly CreateMotoboyValidation _validator = new();

    [Fact]
    public void ShouldReturnErrorWhenNameIsInvalid()
    {
        // Arrange
        var command = CreateMotoboyCommandFaker.Invalid().Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
        result.ShouldHaveValidationErrorFor(x => x.Password);
        result.ShouldHaveValidationErrorFor(x => x.Cnpj);
        result.ShouldHaveValidationErrorFor(x => x.DateBirth);
        result.ShouldHaveValidationErrorFor(x => x.Cnh);
    }

    [Fact]
    public void ShouldNotReturnErrorWhenCommandIsValid()
    {
        // Arrange
        var command = CreateMotoboyCommandFaker.Default().Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
        result.ShouldNotHaveValidationErrorFor(x => x.Password);
        result.ShouldNotHaveValidationErrorFor(x => x.Cnpj);
        result.ShouldNotHaveValidationErrorFor(x => x.DateBirth);
        result.ShouldNotHaveValidationErrorFor(x => x.Cnh);
    }
}