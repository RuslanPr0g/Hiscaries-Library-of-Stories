﻿using HC.Application.Read.Notifications.ReadModels;
using MediatR;

namespace HC.Application.Read.Notifications.Queries;

public sealed class GetUserNotificationsQuery : IRequest<IEnumerable<NotificationReadModel>>
{
    public Guid UserId { get; set; }
}