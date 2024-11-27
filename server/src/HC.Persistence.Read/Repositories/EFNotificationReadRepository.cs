using HC.Application.Read.Notifications.DataAccess;
using HC.Application.Read.Users.ReadModels;
using HC.Domain.UserAccounts;
using HC.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HC.Persistence.Read.Repositories;

public class EFNotificationReadRepository : INotificationReadRepository
{
    private readonly HiscaryContext _context;

    public EFNotificationReadRepository(HiscaryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<NotificationReadModel>> GetMissedNotificationsByUserId(UserAccountId userId) =>
        await _context.Notifications
            .AsNoTracking()
            .Where(x => x.UserId == userId && !x.IsRead)
            .Select(user => NotificationReadModel.FromDomainModel(user))
            .ToListAsync();
}

