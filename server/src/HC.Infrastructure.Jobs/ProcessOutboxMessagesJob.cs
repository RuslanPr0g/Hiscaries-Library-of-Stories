using HC.Application.Common.Outbox;
using HC.Domain.Notifications.Events;
using HC.Persistence.Context;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;

namespace HC.Infrastructure.Jobs;

public class ProcessOutboxMessagesJob : IJob
{
    private readonly HiscaryContext _context;
    private readonly IPublishEndpoint _publisher;

    public ProcessOutboxMessagesJob(HiscaryContext context, IPublishEndpoint publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _context
        .Set<OutboxMessage>()
        .Where(m => m.ProcessedOnUtc == null)
        .Take(20)
        .ToListAsync();

        foreach (var message in messages)
        {
            try
            {
                var messageType = typeof(UserAccountBannedDomainEvent).Assembly.GetType(message.Type);

                if (messageType is null)
                {
                    message.Error = $"Type '{message.Type}' could not be resolved.";
                    continue;
                }

                var domainEvent = JsonConvert.DeserializeObject(message.Content, messageType, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All,
                });

                if (domainEvent is null)
                {
                    continue;
                }

                // TODO: if event handler fails, there is not way (for now) to catch it and retry the message
                await _publisher.Publish(domainEvent);

                message.ProcessedOnUtc = DateTime.UtcNow;
                message.Error = string.Empty;
            }
            catch (Exception ex)
            {
                message.Error = JsonConvert.SerializeObject(ex);
                throw;
            }
        }

        await _context.SaveChangesAsync();
    }
}
