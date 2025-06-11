using Enterprise.Domain.EventPublishers;
using Enterprise.EventHandlers;
using HC.PlatformUsers.Domain.DataAccess;
using HC.PlatformUsers.IntegrationEvents.Outgoing;
using HC.Stories.IntegrationEvents.Outgoing;
using Microsoft.Extensions.Logging;
using Wolverine;

namespace HC.PlatformUsers.EventHandlers.IntegrationEvents;

public sealed class StoryPublishedIntegrationEventHandler(
    IPlatformUserWriteRepository repository,
    IEventPublisher publisher,
    ILogger<StoryPublishedIntegrationEventHandler> logger)
        : IEventHandler<StoryPublishedIntegrationEvent>
{
    private readonly IPlatformUserWriteRepository _platformUserRepository = repository;
    private readonly IEventPublisher _publisher = publisher;

    public async Task Handle(
        StoryPublishedIntegrationEvent integrationEvent, IMessageContext context)
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
