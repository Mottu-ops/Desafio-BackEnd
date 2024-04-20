namespace Motorent.Infrastructure.Common.Outbox;

internal sealed class OutboxMessage
{
    public const int MaxAttempt = 6;

    public static readonly int[] RetryDelays = [0, 1, 5, 10, 30, 60];

    private OutboxMessage()
    {
    }

    public Guid Id { get; init; }

    public string Type { get; init; } = null!;

    public string Data { get; init; } = null!;

    public DateTimeOffset CreatedAt { get; init; }

    public DateTimeOffset? NextAttemptAt { get; private set; }

    public DateTimeOffset? ProcessedAt { get; private set; }

    public OutboxMessageStatus Status { get; private set; }

    public int Attempt { get; private set; }

    public string? ErrorType { get; private set; }

    public string? ErrorMessage { get; private set; }

    public string? ErrorDetails { get; private set; }

    public static OutboxMessage Create<T>(T entity) where T : class => new()
    {
        Id = Guid.NewGuid(),
        Type = entity.GetType().Name,
        Data = OutboxMessageSerializer.Serialize(entity),
        CreatedAt = DateTimeOffset.UtcNow,
        Status = OutboxMessageStatus.Pending,
        Attempt = 1
    };

    public void MarkAsProcessed()
    {
        if (Status == OutboxMessageStatus.Processed || Attempt > MaxAttempt)
        {
            return;
        }

        ProcessedAt = DateTimeOffset.UtcNow;
        Status = OutboxMessageStatus.Processed;
        NextAttemptAt = null;
    }

    public void MarkAsFailed(string errorType, string errorMessage, string errorDetails)
    {
        if (Status == OutboxMessageStatus.Processed)
        {
            return;
        }

        if (Attempt is MaxAttempt)
        {
            throw new OutboxMessageRetryLimitReachedException(Id);
        }

        ErrorType = errorType;
        ErrorMessage = errorMessage;
        ErrorDetails = errorDetails;
        
        Attempt++;
        
        NextAttemptAt = Attempt is MaxAttempt 
            ? null 
            : DateTimeOffset.UtcNow.AddMinutes(RetryDelays[Attempt - 1]);

        ProcessedAt = DateTimeOffset.UtcNow;
        Status = OutboxMessageStatus.Retry;
    }
}