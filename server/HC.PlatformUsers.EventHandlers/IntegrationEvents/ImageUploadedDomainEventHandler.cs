using Enterprise.EventHandlers;
using HC.Notifications.Domain.Events;
using HC.PlatformUsers.Domain.DataAccess;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.PlatformUsers.EventHandlers.IntegrationEvents;

public sealed class ImageUploadedIntegrationEventHandler(
    IPlatformUserWriteRepository repository,
    ILogger<ImageUploadedIntegrationEventHandler> logger)
        : BaseEventHandler<ImageUploadedDomainEvent>(logger)
{
    private readonly IPlatformUserWriteRepository _platformUserRepository = repository;

    protected override async Task HandleEventAsync(
        ImageUploadedDomainEvent integrationEvent,
        ConsumeContext<ImageUploadedDomainEvent> context)
    {
        var imageUrl = integrationEvent.ImageUrl;
        var libraryId = integrationEvent.RequesterId;

        if (string.IsNullOrWhiteSpace(imageUrl))
        {
            return;
        }

        var user = await _platformUserRepository.GetLibraryOwnerByLibraryId(libraryId);

        if (user is null)
        {
            return;
        }

        user.UpdateAvatarUrl(libraryId, imageUrl);

        await _platformUserRepository.SaveChanges();
    }
}
