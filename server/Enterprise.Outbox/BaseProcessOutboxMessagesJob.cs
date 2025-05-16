using Enterprise.Domain.Outbox;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using System.Reflection;

namespace Enterprise.Outbox;

public abstract class BaseProcessOutboxMessagesJob<TContext, TAssembly>(IPublishEndpoint publisher) : IJob
    where TContext : DbContext
    where TAssembly : Assembly
{
    protected abstract TContext Context { get; init; }
    protected abstract TAssembly MessagesAssembly { get; init; }

    private readonly IPublishEndpoint _publisher = publisher;

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await Context
        .Set<OutboxMessage>()
        .Where(m => m.ProcessedOnUtc == null)
        .Take(20)
        .ToListAsync();

        foreach (var message in messages)
        {
            try
            {
                var messageType = MessagesAssembly.GetType(message.Type);

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

        await Context.SaveChangesAsync();
    }
}
