using Enterprise.EventHandlers;
using HC.PlatformUsers.Domain.DataAccess;
using HC.PlatformUsers.Domain.Events;
using HC.Stories.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.PlatformUsers.EventHandlers.IntegrationEvents;

public sealed class StoryPublishedIntegrationEventHandler(
    IPlatformUserWriteRepository repository,
    IPublishEndpoint publisher,
    ILogger<StoryPublishedIntegrationEventHandler> logger)
        : BaseEventHandler<StoryPublishedDomainEvent>(logger)
{
    private readonly IPlatformUserWriteRepository _platformUserRepository = repository;
    private readonly IPublishEndpoint _publisher = publisher;

    protected override async Task HandleEventAsync(
        StoryPublishedDomainEvent integrationEvent,
        ConsumeContext<StoryPublishedDomainEvent> context)
    {
        var userIds = await _platformUserRepository.GetLibrarySubscribersUserAccountIds(integrationEvent.LibraryId);

        var integrationEventForNotification = new UserPublishedStoryDomainEvent(
            userIds.ToArray(),
            integrationEvent.LibraryId,
            integrationEvent.StoryId,
            integrationEvent.Title,
            integrationEvent.PreviewUrl);

        await _publisher.Publish(integrationEventForNotification);
    }
}
