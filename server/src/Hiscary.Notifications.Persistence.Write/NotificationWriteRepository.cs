using Hiscary.Notifications.Domain;
using Hiscary.Notifications.Domain.DataAccess;
using Hiscary.Notifications.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using StackNucleus.DDD.Persistence.EF.Postgres;

namespace Hiscary.Notifications.Persistence.Write;

public class NotificationWriteRepository(NotificationsContext context)
    : BaseWriteRepository<Notification, NotificationId, NotificationsContext>,
    INotificationWriteRepository
{
    protected override NotificationsContext Context { get; init; } = context;

    public async Task<Notification?> GetByObjectReferenceId(Guid objectReferenceId)
    {
        return await Context.Notifications
            .FirstOrDefaultAsync(_ => _.RelatedObjectId == objectReferenceId);
    }
}
