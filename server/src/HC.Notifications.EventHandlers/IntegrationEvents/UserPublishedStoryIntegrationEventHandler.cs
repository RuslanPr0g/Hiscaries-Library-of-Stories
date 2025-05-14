using Enterprise.EventHandlers;
using Enterprise.Generators;
using HC.Notifications.Domain.DataAccess;
using HC.Notifications.Domain.Notifications;
using HC.PlatformUsers.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.Stories.EventHandlers.DomainEvents;

public sealed class UserPublishedStoryIntegrationEventHandler(
    INotificationWriteRepository repository,
    IIdGenerator idGenerator,
    ILogger<UserPublishedStoryIntegrationEventHandler> logger)
        : BaseEventHandler<UserPublishedStoryDomainEvent>(logger)
{
    private readonly INotificationWriteRepository _repository = repository;
    private readonly IIdGenerator _idGenerator = idGenerator;

    protected override async Task HandleEventAsync(
        UserPublishedStoryDomainEvent integrationEvent,
        ConsumeContext<UserPublishedStoryDomainEvent> context)
    {
        var userIds = integrationEvent.SubscriberIds;

        foreach (var userId in userIds)
        {
            var notificationId = _idGenerator.Generate((val) => new NotificationId(val));
            var notification = Notification.CreatePublishedNotification(
                notificationId,
                userId,
                integrationEvent.Title,
                // TODO: should be in consts
                "StoryPublished",
                integrationEvent.StoryId,
                integrationEvent.PreviewUrl);
            await _repository.Add(notification);
        }

        await _repository.SaveChanges();
    }
}
