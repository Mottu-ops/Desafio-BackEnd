using FluentValidation;
using FluentValidation.Results;
using Motorent.Application.Common.Behaviors;

namespace Motorent.Application.UnitTests.Common.Behaviors;

[TestSubject(typeof(ValidationBehavior<,>))]
public sealed class ValidationBehaviorTests
{
    private readonly RequestHandlerDelegate<Result<object>> next = A.Fake<RequestHandlerDelegate<Result<object>>>();
    private readonly IValidator<IRequest<Result<object>>> validator = A.Fake<IValidator<IRequest<Result<object>>>>();

    private readonly CancellationToken cancellationToken = A.Dummy<CancellationToken>();

    private readonly IRequest<Result<object>> request = A.Fake<IRequest<Result<object>>>();

    private readonly object response = new();

    private readonly ValidationBehavior<IRequest<Result<object>>, Result<object>> sut;

    public ValidationBehaviorTests()
    {
        sut = new ValidationBehavior<IRequest<Result<object>>, Result<object>>(validator);
    }

    [Fact]
    public async Task Handle_WhenValidatorIsNull_ShouldCallNext()
    {
        // Arrange
        var sutWithNullValidator = new ValidationBehavior<IRequest<Result<object>>, Result<object>>();

        A.CallTo(() => next())
            .Returns(response);

        // Act
        await sutWithNullValidator.Handle(request, next, cancellationToken);

        // Assert
        A.CallTo(() => next())
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_WhenRequestValidationSucceeds_ShouldCallNext()
    {
        // Arrange
        A.CallTo(() => validator.ValidateAsync(request, cancellationToken))
            .Returns(new ValidationResult());

        A.CallTo(() => next())
            .Returns(response);

        // Act
        await sut.Handle(request, next, cancellationToken);

        // Assert
        A.CallTo(() => validator.ValidateAsync(request, cancellationToken))
            .MustHaveHappenedOnceExactly()
            .Then(A.CallTo(() => next())
                .MustHaveHappenedOnceExactly());
    }

    [Fact]
    public async Task Handle_WhenRequestValidationFails_ShouldNotCallNext()
    {
        // Arrange
        var validationFailures = new List<ValidationFailure>
        {
            new("Property1", "Error1"),
            new("Property2", "Error2")
        };

        A.CallTo(() => validator.ValidateAsync(request, cancellationToken))
            .Returns(new ValidationResult(validationFailures));

        // Act
        await sut.Handle(request, next, cancellationToken);

        // Assert
        A.CallTo(() => validator.ValidateAsync(request, cancellationToken))
            .MustHaveHappenedOnceExactly();

        A.CallTo(() => next())
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task Handle_WhenRequestValidationFails_ShouldReturnValidationErrorsAsResultErrors()
    {
        // Arrange
        var validationFailures = new List<ValidationFailure>
        {
            new("Property1", "Error1"),
            new("Property2", "Error2")
        };

        A.CallTo(() => validator.ValidateAsync(request, cancellationToken))
            .Returns(new ValidationResult(validationFailures));

        // Act
        var result = await sut.Handle(request, next, cancellationToken);

        // Assert
        result.Should().BeFailureSequentially(
        [
            Error.Validation("Error1", "Property1"),
            Error.Validation("Error2", "Property2")
        ]);
    }
}