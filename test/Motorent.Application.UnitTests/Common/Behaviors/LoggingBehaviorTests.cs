using FluentAssertions;
using Microsoft.Extensions.Logging;
using Motorent.Application.Common.Behaviors;

namespace Motorent.Application.UnitTests.Common.Behaviors;

[TestSubject(typeof(LoggingBehavior<,>))]
public sealed class LoggingBehaviorTests
{
    private readonly ILogger<LoggingBehavior<IRequest, object>> logger =
        A.Fake<ILogger<LoggingBehavior<IRequest, object>>>();

    private readonly RequestHandlerDelegate<object> next = A.Fake<RequestHandlerDelegate<object>>();

    private readonly IRequest request = A.Fake<IRequest>();
    private readonly object response = A.Dummy<object>();

    private readonly CancellationToken cancellationToken = A.Dummy<CancellationToken>();

    private readonly LoggingBehavior<IRequest, object> sut;

    public LoggingBehaviorTests()
    {
        sut = new LoggingBehavior<IRequest, object>(logger);

        A.CallTo(() => next())
            .Returns(response);
    }

    [Fact(Skip = "Not implemented yet")]
    public async Task LoggingBehavior_WhenCalled_ShouldLogInformationCorrectly()
    {
        // Arrange
        // Act
        await sut.Handle(request, next, cancellationToken);

        // Assert
    }

    [Fact]
    public async Task LoggingBehavior_WhenCalled_ShouldReturnResponse()
    {
        // Arrange
        // Act
        var result = await sut.Handle(request, next, cancellationToken);

        // Assert
        result.Should().Be(response);
    }
}