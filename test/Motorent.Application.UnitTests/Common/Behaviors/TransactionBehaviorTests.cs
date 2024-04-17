using Microsoft.Extensions.Logging;
using Motorent.Application.Common.Abstractions.Persistence;
using Motorent.Application.Common.Behaviors;
using Motorent.Application.UnitTests.TestUtils.Dummy;

namespace Motorent.Application.UnitTests.Common.Behaviors;

[TestSubject(typeof(TransactionBehavior<,>))]
public sealed class TransactionBehaviorTests
{
    private readonly IUnitOfWork unitOfWork = A.Fake<IUnitOfWork>();

    private readonly ILogger<TransactionBehavior<DummyTransactionalRequest, Result<object>>> logger =
        A.Fake<ILogger<TransactionBehavior<DummyTransactionalRequest, Result<object>>>>();

    private readonly RequestHandlerDelegate<Result<object>> next =
        A.Fake<RequestHandlerDelegate<Result<object>>>();

    private readonly DummyTransactionalRequest request = A.Fake<DummyTransactionalRequest>();
    private readonly object response = A.Dummy<object>();

    private readonly CancellationToken cancellationToken = A.Dummy<CancellationToken>();

    private readonly TransactionBehavior<DummyTransactionalRequest, Result<object>> sut;

    public TransactionBehaviorTests()
    {
        sut = new TransactionBehavior<DummyTransactionalRequest, Result<object>>(unitOfWork, logger);

        A.CallTo(() => next())
            .Returns(response);
    }

    [Fact]
    public async Task TransactionBehavior_WhenNoExceptionOccurs_ShouldPerformTransaction()
    {
        // Act
        await sut.Handle(request, next, cancellationToken);

        // Assert
        A.CallTo(() => unitOfWork.BeginTransactionAsync(cancellationToken))
            .MustHaveHappenedOnceExactly()
            .Then(A.CallTo(() => next())
                .MustHaveHappenedOnceExactly())
            .Then(A.CallTo(() => unitOfWork.CommitTransactionAsync(cancellationToken))
                .MustHaveHappenedOnceExactly());

        A.CallTo(() => unitOfWork.RollbackTransactionAsync(cancellationToken))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task TransactionBehavior_WhenExceptionOccurs_ShouldRollbackTransaction()
    {
        // Arrange
        A.CallTo(() => next())
            .Throws<Exception>();

        // Act
        try
        {
            await sut.Handle(request, next, cancellationToken);
        }
        catch
        {
            // ignored
        }

        // Assert
        A.CallTo(() => unitOfWork.RollbackTransactionAsync(cancellationToken))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task TransactionBehavior_WhenExceptionOccurs_ShouldNotCallNextAgain()
    {
        // Arrange
        A.CallTo(() => next())
            .Throws<Exception>();

        // Act
        try
        {
            await sut.Handle(request, next, cancellationToken);
        }
        catch
        {
            // ignored
        }

        // Assert
        A.CallTo(() => next())
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task TransactionBehavior_WhenExceptionOccurs_ShouldRethrowException()
    {
        // Arrange
        var exception = new Exception();
        A.CallTo(() => next())
            .Throws(exception);

        // Act
        var act = () => sut.Handle(request, next, cancellationToken);

        // Assert
        var thrown = await act.Should().ThrowAsync<Exception>();
        thrown.Which.Should().Be(exception);
    }
}