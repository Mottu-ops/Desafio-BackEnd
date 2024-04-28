using Job.Commons.Domain.Commands.User.Manager;
using Job.Domain.Commands.User.Manager.Validations;

namespace Job.UnitTests.Domain.Commands.User.Manager;

[Trait("Validation", "AuthenticationManagerValidation")]
public class AuthenticationManagerCommandTest
{
    private readonly AuthenticationManagerValidation _validator = new();

    [Fact]
    public void ShouldReturnErrorWhenEmailIsInvalid()
    {
        // Arrange
        var command = AuthenticationManagerCommandFaker.Invalid().Generate();


        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void ShouldNotReturnErrorWhenCommandIsValid()
    {
        // Arrange
        var command = AuthenticationManagerCommandFaker.Default().Generate();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
        result.ShouldNotHaveValidationErrorFor(x => x.Password);
    }
}