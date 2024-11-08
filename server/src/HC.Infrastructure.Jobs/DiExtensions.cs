using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace HC.Quartz;

public static class DiExtensions
{
    public static IServiceCollection AddJobs(this IServiceCollection serviceDescriptors)
    {
        serviceDescriptors.AddQuartz(conf =>
        {
            var outboxKey = new JobKey(nameof(ProcessOutboxMessagesJob));
            var clearanceKey = new JobKey(nameof(CleanOutboxJob));

            conf.AddJob<ProcessOutboxMessagesJob>(outboxKey).AddTrigger(trigger =>
            {
                trigger.ForJob(outboxKey).WithSimpleSchedule(schedule =>
                    schedule.WithIntervalInSeconds(5).RepeatForever());
            });

            conf.AddJob<CleanOutboxJob>(clearanceKey).AddTrigger(trigger =>
            {
                trigger.ForJob(clearanceKey).WithSimpleSchedule(schedule =>
                    schedule.WithIntervalInSeconds(900).RepeatForever());
            });
        });

        serviceDescriptors.AddQuartzHostedService();

        return serviceDescriptors;
    }
}