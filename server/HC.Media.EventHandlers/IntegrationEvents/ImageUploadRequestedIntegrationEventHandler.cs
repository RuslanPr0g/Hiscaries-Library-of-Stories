using Enterprise.Domain.Generators;
using Enterprise.Domain.Images;
using Enterprise.EventHandlers;
using HC.Notifications.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HC.Media.EventHandlers.IntegrationEvents;

public sealed class ImageUploadRequestedIntegrationEventHandler(
    IPublishEndpoint publisher,
    IImageUploader imageUploader,
    IResourceUrlGeneratorService urlGeneratorService,
    IOptions<ResourceSettings> options,
    ResourceSettings settings,
    ILogger<ImageUploadRequestedIntegrationEventHandler> logger)
        : BaseEventHandler<ImageUploadRequestedDomainEvent>(logger)
{
    private readonly string _baseUrl = options.Value.BaseUrl ?? settings.BaseUrl;
    private readonly string _storagePath = options.Value.StoragePath ?? settings.StoragePath;

    private readonly IImageUploader _imageUploader = imageUploader;
    private readonly IResourceUrlGeneratorService _urlGeneratorService = urlGeneratorService;
    private readonly IPublishEndpoint _publisher = publisher;

    protected override async Task HandleEventAsync(
        ImageUploadRequestedDomainEvent integrationEvent,
        ConsumeContext<ImageUploadRequestedDomainEvent> context)
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
        await _publisher.Publish(new ImageUploadedDomainEvent(requesterId, fileUrl));
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

        var storagePath = _storagePath;

        string fileName = await _imageUploader.UploadImageAsync(fileId, imagePreview, type, storagePath);
        return _urlGeneratorService.GenerateImageUrlByFileName(_baseUrl, fileName);
    }
}
