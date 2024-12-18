﻿using HC.Application.Read.Notifications.DataAccess;
using HC.Application.Read.Notifications.ReadModels;
using HC.Domain.Notifications;
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

    public async Task<NotificationReadModel?> GetById(NotificationId id)
    {
        var result = await _context.Notifications
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (result is null)
        {
            return null;
        }

        return NotificationReadModel.FromDomainModel(result);
    }

    public async Task<IEnumerable<NotificationReadModel>> GetMissedNotificationsByUserId(UserAccountId userId) =>
        await _context.Notifications
            .AsNoTracking()
            .Where(x => x.UserId == userId && x.IsRead == false)
            .OrderByDescending(x => x.CreatedAt)
            .Select(user => NotificationReadModel.FromDomainModel(user))
            .ToListAsync();

    public async Task<IEnumerable<NotificationReadModel>> GetNotificationsByUserId(UserAccountId userId) =>
        await _context.Notifications
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .Select(user => NotificationReadModel.FromDomainModel(user))
            .ToListAsync();
}

