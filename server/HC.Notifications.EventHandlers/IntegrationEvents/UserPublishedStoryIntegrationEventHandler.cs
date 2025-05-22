using Enterprise.Domain.Generators;
using Enterprise.EventHandlers;
using HC.Notifications.Domain;
using HC.Notifications.Domain.DataAccess;
using HC.PlatformUsers.IntegrationEvents.Outgoing;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.Notifications.EventHandlers.IntegrationEvents;

public sealed class UserPublishedStoryIntegrationEventHandler(
    INotificationWriteRepository repository,
    IIdGenerator idGenerator,
    ILogger<UserPublishedStoryIntegrationEventHandler> logger)
        : BaseEventHandler<UserPublishedStoryIntegrationEvent>(logger)
{
    private readonly INotificationWriteRepository _repository = repository;
    private readonly IIdGenerator _idGenerator = idGenerator;

    protected override async Task HandleEventAsync(
        UserPublishedStoryIntegrationEvent integrationEvent,
        ConsumeContext<UserPublishedStoryIntegrationEvent> context)
    {
        var userIds = integrationEvent.SubscriberIds;

        var notifications = new List<Notification>();

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
            notifications.Add(notification);
        }

        await _repository.AddRange(notifications.ToArray());

        await _repository.SaveChanges();
    }
}
