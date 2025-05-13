using Enterprise.Persistence.Context;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using System.Reflection;

namespace Enterprise.Outbox;

public class ProcessOutboxMessagesJob : IJob
{
    private readonly EnterpriseContext _context;
    private readonly IPublishEndpoint _publisher;

    public ProcessOutboxMessagesJob(EnterpriseContext context, IPublishEndpoint publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _context
        .OutboxMessages
        .Where(m => m.ProcessedOnUtc == null)
        .Take(20)
        .ToListAsync();

        foreach (var message in messages)
        {
            try
            {
                // TODO: GetCallingAssembly may not work, check!
                // here we need to get event type to properly send the event to message broker
                var messageType = Assembly.GetCallingAssembly().GetType(message.Type);

                if (messageType is null)
                {
                    message.Error = $"Type '{message.Type}' could not be resolved.";
                    continue;
                }

                var domainEvent = JsonConvert.DeserializeObject(
                    message.Content,
                    messageType,
                    new JsonSerializerSettings()
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
