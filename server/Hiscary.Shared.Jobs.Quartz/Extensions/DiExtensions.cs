using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Hiscary.Jobs.Extensions;

public static class DIExtensions
{
    public static IServiceCollection AddConfigurableJobs(
        this IServiceCollection services,
        IEnumerable<JobConfiguration> jobConfigurations)
    {
        services.AddQuartz(conf =>
        {
            foreach (var job in jobConfigurations)
            {
                conf.AddJob(job.Type, new JobKey(job.Key)).AddTrigger(trigger =>
                {
                    var configurator = trigger.ForJob(job.Key);

                    if (job.RepeatInterval > 0)
                    {
                        configurator.WithSimpleSchedule(
                            schedule =>
                            {
                                var scheduleBuilder = schedule
                                    .WithIntervalInSeconds(job.RepeatInterval);

                                if (job.RepeatForever)
                                {
                                    scheduleBuilder.RepeatForever();
                                }
                            });
                    }
                });
            }
        });

        services.AddQuartzHostedService();

        return services;
    }
}
