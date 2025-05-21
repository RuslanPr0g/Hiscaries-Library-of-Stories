using Enterprise.EventHandlers;
using HC.Notifications.Domain.DataAccess;
using HC.Notifications.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.Notifications.EventHandlers.IntegrationEvents;

public sealed class NotificationReferenceObjectIdPreviewChangedDomainEventEventHandler(
    INotificationWriteRepository repository,
    ILogger<NotificationReferenceObjectIdPreviewChangedDomainEventEventHandler> logger)
        : BaseEventHandler<NotificationReferenceObjectIdPreviewChangedDomainEvent>(logger)
{
    private readonly INotificationWriteRepository _repository = repository;

    protected override async Task HandleEventAsync(
        NotificationReferenceObjectIdPreviewChangedDomainEvent integrationEvent,
        ConsumeContext<NotificationReferenceObjectIdPreviewChangedDomainEvent> context)
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
