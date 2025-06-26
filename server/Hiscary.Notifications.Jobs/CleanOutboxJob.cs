using Enterprise.Outbox;
using Hiscary.Notifications.Persistence.Context;

namespace Hiscary.Notifications.Jobs;

internal class CleanOutboxJob(NotificationsContext context) : BaseCleanOutboxJob<NotificationsContext>
{
    protected override NotificationsContext Context { get; init; } = context;
}
