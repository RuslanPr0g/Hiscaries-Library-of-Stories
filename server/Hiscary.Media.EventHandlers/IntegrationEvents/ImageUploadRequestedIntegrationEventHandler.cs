using StackNucleus.DDD.Domain.EventPublishers;
using StackNucleus.DDD.Domain.Images;
using Hiscary.Media.IntegrationEvents.Incoming;
using Hiscary.Media.IntegrationEvents.Outgoing;
using Microsoft.Extensions.Logging;
using Wolverine;

namespace Hiscary.Media.EventHandlers.IntegrationEvents;

public sealed class ImageUploadRequestedIntegrationEventHandler(
    IEventPublisher publisher,
    IImageUploader imageUploader,
    ILogger<ImageUploadRequestedIntegrationEventHandler> logger)
    : IEventHandler<ImageUploadRequestedIntegrationEvent>
{
    private readonly IImageUploader _imageUploader = imageUploader;
    private readonly IEventPublisher _publisher = publisher;
    private readonly ILogger<ImageUploadRequestedIntegrationEventHandler> _logger = logger;

    public async Task Handle(
        ImageUploadRequestedIntegrationEvent integrationEvent, IMessageContext context)
    {
        var file = integrationEvent.Content;
        var requesterId = integrationEvent.RequesterId;

        var fileUrl = await UploadFileAndGetUrlAsync(requesterId, file);

        try
        {
            if (string.IsNullOrWhiteSpace(fileUrl))
            {
                await PublishFail(requesterId, "Could not generate URL. No details can be provided.");
            }
            else
            {
                await PublishSuccess(requesterId, fileUrl);
            }
        }
        catch (Exception exception)
        {
            await PublishFail(requesterId, exception.Message);
        }
    }

    private async Task PublishSuccess(Guid requesterId, string fileUrl)
    {
        await _publisher.Publish(new ImageUploadedIntegrationEvent(requesterId, fileUrl));
    }

    private Task PublishFail(Guid requesterId, string details)
    {
        // TODO: publish event ImageUploadFailedDomainEvent with details
        throw new NotImplementedException();
    }

    private async Task<string?> UploadFileAndGetUrlAsync(
        Guid fileId,
        byte[] imagePreview,
        CancellationToken cancellationToken = default)
    {
        if (imagePreview is null || imagePreview.Length <= 0)
        {
            return null;
        }

        return await _imageUploader.UploadImageAsync(
            fileId,
            imagePreview,
            cancellationToken: cancellationToken
        );
    }
}
