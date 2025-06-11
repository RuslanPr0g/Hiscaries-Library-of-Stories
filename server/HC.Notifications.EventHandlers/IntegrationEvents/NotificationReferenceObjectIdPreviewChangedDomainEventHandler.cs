using Enterprise.EventHandlers;
using HC.Notifications.Domain.DataAccess;
using HC.Notifications.IntegrationEvents.Incoming;
using Microsoft.Extensions.Logging;
using Wolverine;

namespace HC.Notifications.EventHandlers.IntegrationEvents;

public sealed class NotificationReferenceObjectIdPreviewChangedIntegrationEventHandler(
    INotificationWriteRepository repository,
    ILogger<NotificationReferenceObjectIdPreviewChangedIntegrationEventHandler> logger)
        : IEventHandler<NotificationReferenceObjectIdPreviewChangedIntegrationEvent>
{
    private readonly INotificationWriteRepository _repository = repository;

    public async Task Handle(
        NotificationReferenceObjectIdPreviewChangedIntegrationEvent integrationEvent, IMessageContext context)
    {
        var notification = await _repository.GetByObjectReferenceId(integrationEvent.ObjectReferenceId);

        if (notification is null)
        {
            return;
        }

        notification.UpdatePreviewUrl(integrationEvent.PreviewUrl);

        await _repository.SaveChanges();
    }
}
