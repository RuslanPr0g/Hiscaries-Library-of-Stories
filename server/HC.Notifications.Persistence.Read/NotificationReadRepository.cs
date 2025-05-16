using HC.Notifications.Domain.DataAccess;
using HC.Notifications.Domain.ReadModels;
using HC.Notifications.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HC.Notifications.Persistence.Read;

public class NotificationReadRepository(NotificationsContext context) :
    BaseReadRepository<NotificationReadModel>,
    INotificationReadRepository
{
    private NotificationsContext Context { get; init; } = context;

    public async Task<NotificationReadModel?> GetById(Guid id)
    {
        var result = await Context.Notifications
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Value == id);

        if (result is null)
        {
            return null;
        }

        return NotificationReadModel.FromDomainModel(result);
    }

    public async Task<IEnumerable<NotificationReadModel>> GetMissedNotificationsByUserId(Guid userAccountId) =>
        await Context.Notifications
            .AsNoTracking()
            .Where(x => x.UserId == userAccountId && x.IsRead == false)
            .OrderByDescending(x => x.CreatedAt)
            .Select(user => NotificationReadModel.FromDomainModel(user))
            .ToListAsync();

    public async Task<IEnumerable<NotificationReadModel>> GetNotificationsByUserId(Guid userAccountId) =>
        await Context.Notifications
            .AsNoTracking()
            .Where(x => x.UserId == userAccountId)
            .OrderByDescending(x => x.CreatedAt)
            .Select(user => NotificationReadModel.FromDomainModel(user))
            .ToListAsync();
}

