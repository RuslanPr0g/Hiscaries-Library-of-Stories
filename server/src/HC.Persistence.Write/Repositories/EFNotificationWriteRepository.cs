using HC.Application.Write.Notifications.DataAccess;
using HC.Domain.Notifications;
using HC.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HC.Persistence.Write.Repositories;

public class EFNotificationWriteRepository : INotificationWriteRepository
{
    private readonly HiscaryContext _context;

    public EFNotificationWriteRepository(HiscaryContext context)
    {
        _context = context;
    }

    public async Task<Notification?> GetById(NotificationId id) =>
        await _context.Notifications
        .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Notification>> GetByIds(NotificationId[] ids) =>
        await _context.Notifications
        .Where(x => ids.Contains(x.Id))
        .ToListAsync();

    public async Task Add(Notification notification) =>
        await _context.Notifications.AddAsync(notification);
}

