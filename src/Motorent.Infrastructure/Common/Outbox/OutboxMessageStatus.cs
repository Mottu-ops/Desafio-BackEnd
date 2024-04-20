namespace Motorent.Infrastructure.Common.Outbox;

internal enum OutboxMessageStatus
{
    Pending,
    Retry,
    Processed
}