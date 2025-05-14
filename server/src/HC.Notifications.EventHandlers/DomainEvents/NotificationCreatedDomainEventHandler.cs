using Enterprise.EventHandlers;
using HC.Notifications.Domain.DataAccess;
using HC.Notifications.Domain.Notifications.Events;
using HC.Notifications.SignalR.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace HC.Notifications.EventHandlers.DomainEvents;

public sealed class NotificationCreatedDomainEventHandler(
    ILogger<NotificationCreatedDomainEventHandler> logger,
    IHubContext<UserNotificationHub> hubContext,
    INotificationReadRepository repo) : BaseEventHandler<NotificationCreatedDomainEvent>(logger)
{
    private readonly IHubContext<UserNotificationHub> _hubContext = hubContext;
    private readonly INotificationReadRepository _repo = repo;

    protected override async Task HandleEventAsync(
        NotificationCreatedDomainEvent domainEvent,
        ConsumeContext<NotificationCreatedDomainEvent> context)
    {
        var notification = await _repo.GetById(domainEvent.Id);

        if (notification is not null)
        {
            await _hubContext.Clients.User(domainEvent.UserId.ToString()).SendAsync(
                domainEvent.Type,
                notification);
        }
    }
}
