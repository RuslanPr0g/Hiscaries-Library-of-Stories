using Enterprise.Domain.EventPublishers;
using Enterprise.EventHandlers;
using HC.PlatformUsers.Domain.DataAccess;
using HC.PlatformUsers.IntegrationEvents.Outgoing;
using HC.Stories.IntegrationEvents.Outgoing;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.PlatformUsers.EventHandlers.IntegrationEvents;

public sealed class StoryPublishedIntegrationEventHandler(
    IPlatformUserWriteRepository repository,
    IEventPublisher publisher,
    ILogger<StoryPublishedIntegrationEventHandler> logger)
        : BaseEventHandler<StoryPublishedIntegrationEvent>(logger)
{
    private readonly IPlatformUserWriteRepository _platformUserRepository = repository;
    private readonly IEventPublisher _publisher = publisher;

    protected override async Task HandleEventAsync(
        StoryPublishedIntegrationEvent integrationEvent,
        ConsumeContext<StoryPublishedIntegrationEvent> context)
    {
        var userIds = await _platformUserRepository.GetLibrarySubscribersUserAccountIds(integrationEvent.LibraryId);

        var integrationEventForNotification = new UserPublishedStoryIntegrationEvent(
            userIds.ToArray(),
            integrationEvent.LibraryId,
            integrationEvent.StoryId,
            integrationEvent.Title,
            integrationEvent.PreviewUrl);

        await _publisher.Publish(integrationEventForNotification);
    }
}
