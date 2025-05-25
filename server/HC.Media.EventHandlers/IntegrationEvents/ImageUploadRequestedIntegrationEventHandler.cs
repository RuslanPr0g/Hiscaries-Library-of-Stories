using Enterprise.Domain.EventPublishers;
using Enterprise.Domain.Generators;
using Enterprise.Domain.Images;
using Enterprise.EventHandlers;
using HC.Media.Domain;
using HC.Media.IntegrationEvents.Incoming;
using HC.Media.IntegrationEvents.Outgoing;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HC.Media.EventHandlers.IntegrationEvents;

public sealed class ImageUploadRequestedIntegrationEventHandler(
    IEventPublisher publisher,
    IImageUploader imageUploader,
    IResourceUrlGeneratorService urlGeneratorService,
    IOptions<ResourceSettings> options,
    ResourceSettings settings,
    ILogger<ImageUploadRequestedIntegrationEventHandler> logger)
        : BaseEventHandler<ImageUploadRequestedIntegrationEvent>(logger)
{
    private readonly string _baseUrl = options.Value.BaseUrl ?? settings.BaseUrl;

    private readonly IImageUploader _imageUploader = imageUploader;
    private readonly IResourceUrlGeneratorService _urlGeneratorService = urlGeneratorService;
    private readonly IEventPublisher _publisher = publisher;

    protected override async Task HandleEventAsync(
        ImageUploadRequestedIntegrationEvent integrationEvent,
        ConsumeContext<ImageUploadRequestedIntegrationEvent> context)
    {
        var file = integrationEvent.Content;
        var requesterId = integrationEvent.RequesterId;
        var type = integrationEvent.Type;

        var fileUrl = await UploadAvatarAndGetUrlAsync(requesterId, file, type);

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

    private async Task<string?> UploadAvatarAndGetUrlAsync(
        Guid fileId,
        byte[] imagePreview,
        string type)
    {
        if (imagePreview is null || imagePreview.Length <= 0 || string.IsNullOrWhiteSpace(_baseUrl))
        {
            return null;
        }

        string fileName = await _imageUploader.UploadImageAsync(fileId, type, imagePreview);
        return _urlGeneratorService.GenerateImageUrlByFileName(_baseUrl, type, fileName);
    }
}
