using Enterprise.Domain.EventPublishers;
using Enterprise.Outbox;
using HC.Notifications.DomainEvents;
using HC.Notifications.IntegrationEvents;
using HC.Notifications.Persistence.Context;
using System.Reflection;

namespace HC.Notifications.Jobs;

internal class ProcessOutboxMessagesJob(
    NotificationsContext context,
    IEventPublisher publisher) : BaseProcessOutboxMessagesJob<NotificationsContext, Assembly>(publisher)
{
    protected override NotificationsContext Context { get; init; } = context;
    protected override IReadOnlyList<Assembly> MessagesAssembly { get; init; } =
        [typeof(NotificationDomainEventsAssembly).Assembly,
        typeof(NotificationIntegrationEventsAssembly).Assembly,];
}
