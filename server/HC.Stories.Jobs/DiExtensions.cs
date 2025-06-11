using Microsoft.Extensions.DependencyInjection;
using Enterprise.Jobs;

namespace HC.Stories.Jobs;

public static class DiExtensions
{
    public static IServiceCollection AddJobs(this IServiceCollection services)
    {
        return services.AddCommonJobs([
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