using Enterprise.Outbox;
using HC.Notifications.Domain.Events;
using HC.Notifications.Persistence.Context;
using MassTransit;
using System.Reflection;

namespace HC.Notifications.Jobs;

internal class ProcessOutboxMessagesJob(
    NotificationsContext context,
    IPublishEndpoint publisher) : BaseProcessOutboxMessagesJob<NotificationsContext, Assembly>(publisher)
{
    protected override NotificationsContext Context { get; init; } = context;
    protected override Assembly MessagesAssembly { get; init; } = typeof(NotificationCreatedDomainEvent).Assembly;
}
