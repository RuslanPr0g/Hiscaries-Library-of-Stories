using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Enterprise.Outbox;

public static class DiExtensions
{
    public static IServiceCollection AddEnterpriseOutbox(this IServiceCollection serviceDescriptors)
    {
        serviceDescriptors.AddQuartz(conf =>
        {
            var outboxKey = new JobKey(nameof(ProcessOutboxMessagesJob));
            var clearanceKey = new JobKey(nameof(CleanOutboxJob));

            // TODO: use configuration from CI/CD

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