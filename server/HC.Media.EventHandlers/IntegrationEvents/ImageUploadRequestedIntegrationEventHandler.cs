using Enterprise.Domain.Generators;
using Enterprise.Domain.Images;
using Enterprise.EventHandlers;
using HC.Notifications.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.Media.EventHandlers.IntegrationEvents;

public sealed class ImageUploadRequestedIntegrationEventHandler(
    IIdGenerator idGenerator,
    IImageUploader imageUploader,
    IResourceUrlGeneratorService urlGeneratorService,
    ILogger<ImageUploadRequestedIntegrationEventHandler> logger)
        : BaseEventHandler<ImageUploadRequestedDomainEvent>(logger)
{
    private readonly IIdGenerator _idGenerator = idGenerator;
    private readonly IImageUploader _imageUploader = imageUploader;
    private readonly IResourceUrlGeneratorService _urlGeneratorService = urlGeneratorService;

    protected override async Task HandleEventAsync(
        ImageUploadRequestedDomainEvent integrationEvent,
        ConsumeContext<ImageUploadRequestedDomainEvent> context)
    {
        UploadAvatarAndGetUrlAsync()
    }

    private async Task<string?> UploadAvatarAndGetUrlAsync(Guid libraryId, byte[] imagePreview)
    {
        if (imagePreview.Length == 0)
        {
            return null;
        }

        string fileName = await _imageUploader.UploadImageAsync(libraryId, imagePreview, "users");
        return _urlGeneratorService.GenerateImageUrlByFileName(fileName);
    }
}
