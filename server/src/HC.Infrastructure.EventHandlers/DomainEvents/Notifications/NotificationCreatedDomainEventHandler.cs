using HC.Application.Common.EventHandlers;
using HC.Application.Write.DataAccess;
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

    public NotificationCreatedDomainEventHandler(
        ILogger<NotificationCreatedDomainEventHandler> logger,
        IUnitOfWork unitOfWork)
        : base(logger, unitOfWork)
    {
    }

    protected override async Task HandleEventAsync(NotificationCreatedDomainEvent domainEvent, ConsumeContext<NotificationCreatedDomainEvent> context)
    {
        await _hubContext.Clients.User(domainEvent.UserId.ToString()).SendAsync(domainEvent.Type, domainEvent.Message);
    }
}
