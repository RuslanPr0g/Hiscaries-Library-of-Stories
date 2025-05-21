using HC.Notifications.Domain;
using HC.Notifications.Domain.DataAccess;
using HC.Notifications.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HC.Notifications.Persistence.Write;

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
