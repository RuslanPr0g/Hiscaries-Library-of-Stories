using Enterprise.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;

namespace Enterprise.Outbox;

public class CleanOutboxJob : IJob
{
    private readonly EnterpriseContext _context;

    public CleanOutboxJob(EnterpriseContext context)
    {
        _context = context;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _context
        .OutboxMessages
        .Where(m => m.ProcessedOnUtc != null)
        .Take(5)
        .ToListAsync();

        foreach (var message in messages)
        {
            try
            {
                _context.Remove(message);
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
