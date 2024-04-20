using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Motorent.Domain.Common.Events;
using Motorent.Infrastructure.Common.Outbox;
using Quartz;

namespace Motorent.Infrastructure.Common.Persistence.BackgroundJobs;

internal sealed class ProcessOutboxMessagesJob(
    DataContext dataContext,
    IPublisher publisher,
    ILogger<ProcessOutboxMessagesJob> logger) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await FetchIncomingOutboxMessagesAsync(context.CancellationToken);
        if (messages.Count is 0)
        {
            return;
        }

        foreach (var message in messages)
        {
            logger.LogInformation("Processing outbox message {MessageId}. Attempt: {Attempt}",
                message.Id, message.Attempt);

            var @event = OutboxMessageSerializer.Deserialize<IEvent>(message.Data);
            if (@event is null)
            {
                logger.LogError("Failed to deserialize Outbox message ({@Message}) to type {Type}",
                    message, typeof(IEvent));

                continue;
            }

            try
            {
                await publisher.Publish(@event, context.CancellationToken);
                message.MarkAsProcessed();

                logger.LogInformation("Outbox message {MessageId} has been successfully processed", message.Id);
            }
            catch (Exception ex)
            {
                message.MarkAsFailed(
                    errorType: ex.GetType().Name,
                    errorMessage: ex.Message,
                    errorDetails: ex.ToString());

                logger.LogError(ex, "Failed to process outbox message ({@Message})", message);
            }
        }

        await dataContext.SaveChangesAsync(context.CancellationToken);
    }

    private Task<List<OutboxMessage>> FetchIncomingOutboxMessagesAsync(CancellationToken cancellationToken)
    {
        return dataContext
            .Set<OutboxMessage>()
            .Where(om => om.Status == OutboxMessageStatus.Pending
                         || (om.Status == OutboxMessageStatus.Retry && om.NextAttemptAt != null
                                                                    && om.NextAttemptAt <= DateTime.UtcNow))
            .OrderBy(om => om.CreatedAt)
            .Take(20)
            .ToListAsync(cancellationToken);
    }
}