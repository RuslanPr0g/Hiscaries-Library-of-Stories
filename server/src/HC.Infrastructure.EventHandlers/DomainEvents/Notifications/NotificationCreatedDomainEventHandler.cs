using HC.Application.Common.EventHandlers;
using HC.Application.Write.DataAccess;
using HC.Application.Write.Notifications.DataAccess;
using HC.Domain.Notifications.Events;
using HC.Infrastructure.SignalR.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace HC.Infrastructure.EventHandlers.DomainEvents.Users;

// TODO: do I want to allow one domain handler to handle multiple domain events? if no, then this approach is kinda okay
public sealed class NotificationCreatedDomainEventHandler
    : DomainEventHandler<NotificationCreatedDomainEvent>
{
    private readonly IHubContext<UserNotificationHub> _hubContext;
    private readonly INotificationWriteRepository _repo;

    public NotificationCreatedDomainEventHandler(
        ILogger<NotificationCreatedDomainEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<UserNotificationHub> hubContext,
        INotificationWriteRepository repo)
        : base(logger, unitOfWork)
    {
        _hubContext = hubContext;
        _repo = repo;
    }

    protected override async Task HandleEventAsync(NotificationCreatedDomainEvent domainEvent, ConsumeContext<NotificationCreatedDomainEvent> context)
    {
        var notification = await _repo.GetById(domainEvent.Id);

        if (notification is not null)
        {
            await _hubContext.Clients.User(domainEvent.UserId.ToString()).SendAsync(domainEvent.Type, notification);
        }
    }
}
