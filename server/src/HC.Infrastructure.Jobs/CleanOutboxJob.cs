﻿using HC.Application.Common.Outbox;
using HC.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;

namespace HC.Infrastructure.Jobs;

public class CleanOutboxJob : IJob
{
    private readonly HiscaryContext _context;

    public CleanOutboxJob(HiscaryContext context)
    {
        _context = context;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _context
        .Set<OutboxMessage>()
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
