using Motorent.Infrastructure.Common.Outbox;

namespace Motorent.Infrastructure.UnitTests.Common.Outbox;

[TestSubject(typeof(OutboxMessage))]
public sealed class OutboxMessageTests
{
    [Fact]
    public void MarkAsProcessed_WhenStatusIsPending_ShouldMarkAsProcessed()
    {
        // Arrange
        var message = OutboxMessage.Create(new DummyOutboxEntity());

        // Act
        message.MarkAsProcessed();

        // Assert
        message.Status.Should().Be(OutboxMessageStatus.Processed);
        message.ProcessedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        message.Attempt.Should().Be(1);
        message.NextAttemptAt.Should().BeNull();
    }

    [Fact]
    public void MarkAsProcessed_WhenStatusIsRetryAndIsSecondAttempt_ShouldMarkAsProcessed()
    {
        // Arrange
        var message = OutboxMessage.Create(new DummyOutboxEntity());
        message.MarkAsFailed("Error type", "Error message", "Error details");

        // Act
        message.MarkAsProcessed();

        // Assert
        message.Status.Should().Be(OutboxMessageStatus.Processed);
        message.ProcessedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        message.Attempt.Should().Be(2);
        message.NextAttemptAt.Should().BeNull();
    }

    [Fact]
    public void MarkAsProcessed_WhenStatusIsRetryAndIsIntermediateAttempt_ShouldMarkAsProcessed()
    {
        // Arrange
        var message = OutboxMessage.Create(new DummyOutboxEntity());
        var count = (int)Math.Floor(OutboxMessage.MaxAttempt / 2.0);

        for (var i = 1; i < count; i++)
        {
            message.MarkAsFailed(
                $"Error type {i}",
                $"Error message {i}",
                $"Error details {i}");
        }

        // Act
        message.MarkAsProcessed();

        // Assert
        message.Status.Should().Be(OutboxMessageStatus.Processed);
        message.ProcessedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        message.Attempt.Should().Be(count);
        message.NextAttemptAt.Should().BeNull();
    }

    [Fact]
    public void MarkAsProcessed_WhenStatusIsRetryAndIsLastAttempt_ShouldMarkAsProcessed()
    {
        // Arrange
        var message = OutboxMessage.Create(new DummyOutboxEntity());

        for (var i = 1; i < OutboxMessage.MaxAttempt; i++)
        {
            message.MarkAsFailed(
                $"Error type {i}",
                $"Error message {i}",
                $"Error details {i}");
        }

        // Act
        message.MarkAsProcessed();

        // Assert
        message.Status.Should().Be(OutboxMessageStatus.Processed);
        message.ProcessedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        message.Attempt.Should().Be(OutboxMessage.MaxAttempt);
        message.NextAttemptAt.Should().BeNull();
    }

    [Fact]
    public void MarkAsProcessed_WhenStatusIsProcessed_ShouldNotChangeValues()
    {
        // Arrange
        var message = OutboxMessage.Create(new DummyOutboxEntity());
        message.MarkAsProcessed();
        var processedAt = message.ProcessedAt;

        // Act
        message.MarkAsProcessed();

        // Assert
        message.Status.Should().Be(OutboxMessageStatus.Processed);
        message.ProcessedAt.Should().Be(processedAt);
        message.NextAttemptAt.Should().BeNull();
        message.Attempt.Should().Be(1);
    }

    [Fact]
    public void MarkAsFailed_WhenStatusIsPending_ShouldMarkAsFailed()
    {
        // Arrange
        var message = OutboxMessage.Create(new DummyOutboxEntity());

        // Act
        message.MarkAsFailed("Error type", "Error message", "Error details");

        // Assert
        message.Status.Should().Be(OutboxMessageStatus.Retry);
        message.ProcessedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        message.NextAttemptAt.Should().BeCloseTo(DateTimeOffset.UtcNow.AddMinutes(OutboxMessage.RetryDelays[1]),
            TimeSpan.FromSeconds(1));
        message.Attempt.Should().Be(2);
        message.ErrorType.Should().Be("Error type");
        message.ErrorMessage.Should().Be("Error message");
        message.ErrorDetails.Should().Be("Error details");
    }

    [Fact]
    public void MarkAsFailed_WhenStatusIsProcessed_ShouldNotChangeValues()
    {
        // Arrange
        var message = OutboxMessage.Create(new DummyOutboxEntity());
        message.MarkAsProcessed();
        var processedAt = message.ProcessedAt;

        // Act
        message.MarkAsFailed("Error type", "Error message", "Error details");

        // Assert
        message.Status.Should().Be(OutboxMessageStatus.Processed);
        message.ProcessedAt.Should().Be(processedAt);
        message.NextAttemptAt.Should().BeNull();
        message.Attempt.Should().Be(1);
        message.ErrorType.Should().BeNull();
        message.ErrorMessage.Should().BeNull();
        message.ErrorDetails.Should().BeNull();
    }

    [Fact]
    public void MarkAsFailed_WhenStatusIsRetryAndIsFirstAttempt_ShouldMarkAsFailed()
    {
        // Arrange
        var message = OutboxMessage.Create(new DummyOutboxEntity());
        message.MarkAsFailed("Error type", "Error message", "Error details");

        // Act
        message.MarkAsFailed("Error type 2", "Error message 2", "Error details 2");

        // Assert
        message.Status.Should().Be(OutboxMessageStatus.Retry);
        message.ProcessedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        message.NextAttemptAt.Should().BeCloseTo(DateTimeOffset.UtcNow.AddMinutes(OutboxMessage.RetryDelays[2]),
            TimeSpan.FromSeconds(1));
        message.Attempt.Should().Be(3);
        message.ErrorType.Should().Be("Error type 2");
        message.ErrorMessage.Should().Be("Error message 2");
        message.ErrorDetails.Should().Be("Error details 2");
    }

    [Fact]
    public void MarkAsFailed_WhenStatusIsRetryAndIsIntermediateAttempt_ShouldMarkAsFailed()
    {
        // Arrange
        var message = OutboxMessage.Create(new DummyOutboxEntity());
        var count = (int)Math.Floor(OutboxMessage.MaxAttempt / 2.0);
        
        for (var i = 1; i < count; i++)
        {
            message.MarkAsFailed(
                $"Error type {i}",
                $"Error message {i}",
                $"Error details {i}");
        }

        // Act
        message.MarkAsFailed("Error type", "Error message", "Error details");

        // Assert
        message.Status.Should().Be(OutboxMessageStatus.Retry);
        message.ProcessedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        message.NextAttemptAt.Should().BeCloseTo(DateTimeOffset.UtcNow.AddMinutes(OutboxMessage.RetryDelays[count]),
            TimeSpan.FromSeconds(1));
        message.Attempt.Should().Be(4);
        message.ErrorType.Should().Be("Error type");
        message.ErrorMessage.Should().Be("Error message");
        message.ErrorDetails.Should().Be("Error details");
    }
    
    [Fact]
    public void MarkAsFailed_WhenStatusIsRetryAndIsNextToLastAttempt_ShouldMarkAsFailedButNotSetNextAttemptAt()
    {
        // Arrange
        var message = OutboxMessage.Create(new DummyOutboxEntity());
        var count = OutboxMessage.MaxAttempt - 1;
        
        for (var i = 1; i < count; i++)
        {
            message.MarkAsFailed(
                $"Error type {i}",
                $"Error message {i}",
                $"Error details {i}");
        }

        // Act
        message.MarkAsFailed("Error type", "Error message", "Error details");

        // Assert
        message.Status.Should().Be(OutboxMessageStatus.Retry);
        message.ProcessedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        message.NextAttemptAt.Should().BeNull();
        message.Attempt.Should().Be(count + 1);
        message.ErrorType.Should().Be("Error type");
        message.ErrorMessage.Should().Be("Error message");
        message.ErrorDetails.Should().Be("Error details");
    }

    [Fact]
    public void MarkAsFailed_WhenStatusIsRetryAndIsLastAttempt_ShouldThrowOutboxMessageRetryLimitReachedException()
    {
        // Arrange
        var message = OutboxMessage.Create(new DummyOutboxEntity());

        for (var i = 1; i < OutboxMessage.MaxAttempt; i++)
        {
            message.MarkAsFailed(
                $"Error type {i}",
                $"Error message {i}",
                $"Error details {i}");
        }

        // Act
        var act = () => message.MarkAsFailed("Error type", "Error message", "Error details");

        // Assert
        act.Should().Throw<OutboxMessageRetryLimitReachedException>();
    }
}