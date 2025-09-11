using Hiscary.Notifications.Persistence.Context;
using StackNucleus.DDD.Outbox.Jobs;

namespace Hiscary.Notifications.Jobs;

internal class CleanOutboxJob(NotificationsContext context) : BaseCleanOutboxJob<NotificationsContext>
{
    protected override NotificationsContext Context { get; init; } = context;
}
