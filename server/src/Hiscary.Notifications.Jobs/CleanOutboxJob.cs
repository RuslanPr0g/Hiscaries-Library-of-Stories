using Hiscary.Notifications.Persistence.Context;
using Hiscary.Shared.Outbox;

namespace Hiscary.Notifications.Jobs;

internal class CleanOutboxJob(NotificationsContext context) : BaseCleanOutboxJob<NotificationsContext>
{
    protected override NotificationsContext Context { get; init; } = context;
}
