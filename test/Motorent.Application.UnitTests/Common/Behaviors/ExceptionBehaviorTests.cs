using Microsoft.Extensions.Logging;
using Motorent.Application.Common.Behaviors;

namespace Motorent.Application.UnitTests.Common.Behaviors;

[TestSubject(typeof(ExceptionBehavior<,>))]
public sealed class ExceptionBehaviorTests
{
    private readonly ILogger<ExceptionBehavior<IRequest<Result<object>>, Result<object>>> logger =
        A.Fake<ILogger<ExceptionBehavior<IRequest<Result<object>>, Result<object>>>>();

    private readonly RequestHandlerDelegate<Result<object>> next =
        A.Fake<RequestHandlerDelegate<Result<object>>>();

    private readonly IRequest<Result<object>> request = A.Fake<IRequest<Result<object>>>();
    private readonly object response = A.Dummy<object>();

    private readonly CancellationToken cancellationToken = A.Dummy<CancellationToken>();

    private readonly ExceptionBehavior<IRequest<Result<object>>, Result<object>> sut;

    public ExceptionBehaviorTests()
    {
        sut = new ExceptionBehavior<IRequest<Result<object>>, Result<object>>(logger);

        A.CallTo(() => next())
            .Returns(response);
    }

    [Fact]
    public async Task ExceptionBehavior_WhenNoExceptionOccurs_ShouldReturnResponse()
    {
        // Arrange
        // Act
        var result = await sut.Handle(request, next, cancellationToken);

        // Assert
        result.Should().BeSuccess(response);
    }

    [Fact]
    public async Task ExceptionBehavior_WhenExceptionOccurs_ShouldNotCallNextAgain()
    {
        // Arrange
        A.CallTo(() => next())
            .Throws<Exception>();

        // Act
        await sut.Handle(request, next, cancellationToken);

        // Assert
        A.CallTo(() => next())
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task ExceptionBehavior_WhenExceptionOccurs_ShouldReturnFailure()
    {
        // Arrange
        A.CallTo(() => next())
            .Throws<Exception>();

        // Act
        var result = await sut.Handle(request, next, cancellationToken);

        // Assert
        result.Should().BeFailure();
    }

    [Fact(Skip = "Not implemented yet")]
    public async Task ExceptionBehavior_WhenExceptionOccurs_ShouldLogError()
    {
        // Arrange
        A.CallTo(() => next())
            .Throws<Exception>();

        // Act
        await sut.Handle(request, next, cancellationToken);

        // Assert
    }
}