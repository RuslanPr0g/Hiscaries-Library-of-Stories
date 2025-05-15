using Enterprise.Outbox;
using HC.Notifications.Persistence.Context;

namespace HC.Notifications.Jobs;

internal class CleanOutboxJob(NotificationsContext context) : BaseCleanOutboxJob<NotificationsContext>
{
    protected override NotificationsContext Context { get; init; } = context;
}
