using Enterprise.EventHandlers;
using HC.Notifications.Domain.Events;
using HC.Stories.Domain.DataAccess;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.Stories.EventHandlers.IntegrationEvents;

public sealed class ImageUploadedIntegrationEventHandler(
    IStoryWriteRepository repository,
    ILogger<ImageUploadedIntegrationEventHandler> logger)
        : BaseEventHandler<ImageUploadedDomainEvent>(logger)
{
    private readonly IStoryWriteRepository _repository = repository;

    protected override async Task HandleEventAsync(
        ImageUploadedDomainEvent integrationEvent,
        ConsumeContext<ImageUploadedDomainEvent> context)
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

        await _repository.SaveChanges();
    }
}
