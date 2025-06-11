using HC.Notifications.Domain.DataAccess;
using HC.Notifications.DomainEvents;
using HC.Notifications.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Wolverine;

namespace HC.Notifications.EventHandlers.DomainEvents;

public sealed class NotificationCreatedDomainEventHandler(
    ILogger<NotificationCreatedDomainEventHandler> logger,
    IHubContext<UserNotificationHub> hubContext,
    INotificationReadRepository repo) : IEventHandler<NotificationCreatedDomainEvent>
{
    private readonly ILogger<NotificationCreatedDomainEventHandler> _logger = logger;
    private readonly IHubContext<UserNotificationHub> _hubContext = hubContext;
    private readonly INotificationReadRepository _repo = repo;

    public async Task Handle(
        NotificationCreatedDomainEvent domainEvent, IMessageContext context)
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
