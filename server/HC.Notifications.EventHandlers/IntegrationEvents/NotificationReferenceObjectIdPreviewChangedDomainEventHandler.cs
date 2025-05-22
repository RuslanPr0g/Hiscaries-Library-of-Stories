using Enterprise.EventHandlers;
using HC.Notifications.Domain.DataAccess;
using HC.Notifications.IntegrationEvents.Incoming;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.Notifications.EventHandlers.IntegrationEvents;

public sealed class NotificationReferenceObjectIdPreviewChangedIntegrationEventHandler(
    INotificationWriteRepository repository,
    ILogger<NotificationReferenceObjectIdPreviewChangedIntegrationEventHandler> logger)
        : BaseEventHandler<NotificationReferenceObjectIdPreviewChangedIntegrationEvent>(logger)
{
    private readonly INotificationWriteRepository _repository = repository;

    protected override async Task HandleEventAsync(
        NotificationReferenceObjectIdPreviewChangedIntegrationEvent integrationEvent,
        ConsumeContext<NotificationReferenceObjectIdPreviewChangedIntegrationEvent> context)
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
