﻿using Hiscary.Media.IntegrationEvents.Outgoing;
using Hiscary.Notifications.IntegrationEvents.Incoming;
using Hiscary.Stories.Domain.DataAccess;
using Microsoft.Extensions.Logging;
using StackNucleus.DDD.Domain.EventHandlers;
using StackNucleus.DDD.Domain.EventPublishers;
using Wolverine;

namespace Hiscary.Stories.EventHandlers.IntegrationEvents;

public sealed class ImageUploadedIntegrationEventHandler(
    IEventPublisher publisher,
    IStoryWriteRepository repository,
    ILogger<ImageUploadedIntegrationEventHandler> logger)
        : IEventHandler<ImageUploadedIntegrationEvent>
{
    private readonly IEventPublisher _publisher = publisher;
    private readonly IStoryWriteRepository _repository = repository;

    public async Task Handle(
        ImageUploadedIntegrationEvent integrationEvent, IMessageContext context)
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
