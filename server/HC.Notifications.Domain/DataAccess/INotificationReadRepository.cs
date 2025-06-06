﻿using Enterprise.Domain.DataAccess;
using HC.Notifications.Domain.ReadModels;

namespace HC.Notifications.Domain.DataAccess;

public interface INotificationReadRepository : IBaseReadRepository<NotificationReadModel>
{
    Task<NotificationReadModel?> GetById(Guid id);
    Task<IEnumerable<NotificationReadModel>> GetMissedNotificationsByUserId(Guid userAccountId);
    Task<IEnumerable<NotificationReadModel>> GetNotificationsByUserId(Guid userAccountId);
}
