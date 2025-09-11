using Microsoft.Extensions.DependencyInjection;
using StackNucleus.DDD.Jobs.Quartz.Extensions;

namespace Hiscary.UserAccounts.Jobs;

public static class DIModule
{
    public static IServiceCollection AddJobs(this IServiceCollection services)
    {
        return services.AddConfigurableJobs([
            new JobConfiguration
            {
                Key = nameof(ProcessOutboxMessagesJob),
                Type = typeof(ProcessOutboxMessagesJob),
                RepeatInterval = 5,
                RepeatForever = true
            },
            new JobConfiguration
            {
                Key = nameof(CleanOutboxJob),
                Type = typeof(CleanOutboxJob),
                RepeatInterval = 900,
                RepeatForever = true
            }
        ]);
    }
}