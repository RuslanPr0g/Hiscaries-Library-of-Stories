using Hiscary.Media.IntegrationEvents.Outgoing;
using Hiscary.PlatformUsers.Domain.DataAccess;
using Microsoft.Extensions.Logging;
using Wolverine;

namespace Hiscary.PlatformUsers.EventHandlers.IntegrationEvents;

public sealed class ImageUploadedIntegrationEventHandler(
    IPlatformUserWriteRepository repository,
    ILogger<ImageUploadedIntegrationEventHandler> logger)
        : IEventHandler<ImageUploadedIntegrationEvent>
{
    private readonly IPlatformUserWriteRepository _platformUserRepository = repository;

    public async Task Handle(
        ImageUploadedIntegrationEvent integrationEvent, IMessageContext context)
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
