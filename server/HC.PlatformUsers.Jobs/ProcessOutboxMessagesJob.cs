using Enterprise.Domain.EventPublishers;
using Enterprise.Outbox;
using HC.Media.IntegrationEvents;
using HC.PlatformUsers.DomainEvents;
using HC.PlatformUsers.IntegrationEvents;
using HC.PlatformUsers.Persistence.Context;
using System.Reflection;

namespace HC.PlatformUsers.Jobs;

internal class ProcessOutboxMessagesJob(
    PlatformUsersContext context,
    IEventPublisher publisher) : BaseProcessOutboxMessagesJob<PlatformUsersContext, Assembly>(publisher)
{
    protected override PlatformUsersContext Context { get; init; } = context;
    protected override IReadOnlyList<Assembly> MessagesAssembly { get; init; } = 
        [
        typeof(PlatformUserDomainEventsAssembly).Assembly,
        typeof(PlatformUserIntegrationEventsAssembly).Assembly,
        typeof(MediaIntegrationEventsAssembly).Assembly];
}
