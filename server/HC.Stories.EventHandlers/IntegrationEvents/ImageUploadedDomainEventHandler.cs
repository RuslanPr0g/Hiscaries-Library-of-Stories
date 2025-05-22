using Enterprise.Domain.EventPublishers;
using Enterprise.EventHandlers;
using HC.Media.IntegrationEvents.Outgoing;
using HC.Notifications.IntegrationEvents.Incoming;
using HC.Stories.Domain.DataAccess;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.Stories.EventHandlers.IntegrationEvents;

public sealed class ImageUploadedIntegrationEventHandler(
    IEventPublisher publisher,
    IStoryWriteRepository repository,
    ILogger<ImageUploadedIntegrationEventHandler> logger)
        : BaseEventHandler<ImageUploadedIntegrationEvent>(logger)
{
    private readonly IEventPublisher _publisher = publisher;
    private readonly IStoryWriteRepository _repository = repository;

    protected override async Task HandleEventAsync(
        ImageUploadedIntegrationEvent integrationEvent,
        ConsumeContext<ImageUploadedIntegrationEvent> context)
    {
        var imageUrl = integrationEvent.ImageUrl;
        var storyId = integrationEvent.RequesterId;

        if (string.IsNullOrWhiteSpace(imageUrl))
        {
            return;
        }

        var story = await _repository.GetById(storyId);

        if (story is null)
        {
            return;
        }

        story.UpdatePreviewUrl(imageUrl);

        await _publisher.Publish(
            new NotificationReferenceObjectIdPreviewChangedIntegrationEvent(storyId, imageUrl));

        await _repository.SaveChanges();
    }
}
