using Hiscary.Domain;
using Hiscary.Domain.EventPublishers;
using Hiscary.Domain.Outbox;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using System.Reflection;

namespace Hiscary.Outbox;

public abstract class BaseProcessOutboxMessagesJob<TContext, TAssembly>(IEventPublisher publisher) : IJob
    where TContext : DbContext
    where TAssembly : Assembly
{
    protected abstract TContext Context { get; init; }
    protected abstract IReadOnlyList<TAssembly> MessagesAssembly { get; init; }

    private readonly IEventPublisher _publisher = publisher;

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
                var messageType = MessagesAssembly
                    .Select(asm => asm.GetType(message.Type))
                    .FirstOrDefault(t => t != null);


                if (messageType is null)
                {
                    message.Error = $"Type '{message.Type}' could not be resolved.";
                    continue;
                }

                var @event = JsonConvert.DeserializeObject(
                    message.Content,
                    messageType,
                    new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All,
                    });

                if (@event is null || @event is not IDomainEvent domainEvent)
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

        if (messages.Count > 0)
        {
            await Context.SaveChangesAsync();
        }
    }
}
