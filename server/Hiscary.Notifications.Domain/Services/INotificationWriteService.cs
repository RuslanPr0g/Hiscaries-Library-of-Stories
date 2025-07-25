﻿using StackNucleus.DDD.Domain.ResultModels;

namespace Hiscary.Notifications.Domain.Services;

public interface INotificationWriteService
{
    Task<OperationResult> ReadNotifications(Guid userId, Guid[] notificationIds);
}