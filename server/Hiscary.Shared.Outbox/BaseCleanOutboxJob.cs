using Enterprise.Domain.Outbox;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;

namespace Enterprise.Outbox;

public abstract class BaseCleanOutboxJob<TContext> : IJob
    where TContext : DbContext
{
    protected abstract TContext Context { get; init; }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await Context
        .Set<OutboxMessage>()
        .Where(m => m.ProcessedOnUtc != null)
        .Take(5)
        .ToListAsync();

        foreach (var message in messages)
        {
            try
            {
                Context.Remove(message);
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
