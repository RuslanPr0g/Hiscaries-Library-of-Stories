using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Enterprise.Jobs;

public static class DiExtensions
{
    public static IServiceCollection AddEnterpriseJobs(
        this IServiceCollection serviceDescriptors,
        IEnumerable<JobConfiguration> jobConfigurations)
    {
        serviceDescriptors.AddQuartz(conf =>
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

        serviceDescriptors.AddQuartzHostedService();

        return serviceDescriptors;
    }
}
