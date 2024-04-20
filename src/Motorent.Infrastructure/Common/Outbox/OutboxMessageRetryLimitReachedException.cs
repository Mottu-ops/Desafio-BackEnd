namespace Motorent.Infrastructure.Common.Outbox;

internal sealed class OutboxMessageRetryLimitReachedException(Guid messageId)
    : Exception($"Retry attempt limit reached for outbox message '{messageId}'");