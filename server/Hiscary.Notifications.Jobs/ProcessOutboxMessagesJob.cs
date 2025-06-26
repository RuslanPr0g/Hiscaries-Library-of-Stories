using Enterprise.Domain.EventPublishers;
using Enterprise.Outbox;
using Hiscary.Notifications.DomainEvents;
using Hiscary.Notifications.IntegrationEvents;
using Hiscary.Notifications.Persistence.Context;
using System.Reflection;

namespace Hiscary.Notifications.Jobs;

internal class ProcessOutboxMessagesJob(
    NotificationsContext context,
    IEventPublisher publisher) : BaseProcessOutboxMessagesJob<NotificationsContext, Assembly>(publisher)
{
    protected override NotificationsContext Context { get; init; } = context;
    protected override IReadOnlyList<Assembly> MessagesAssembly { get; init; } =
        [typeof(NotificationDomainEventsAssembly).Assembly,
        typeof(NotificationIntegrationEventsAssembly).Assembly,];
}
