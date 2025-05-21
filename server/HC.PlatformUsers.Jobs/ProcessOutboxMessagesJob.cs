using Enterprise.Outbox;
using HC.Media.Domain.Events;
using HC.PlatformUsers.Domain.Events;
using HC.PlatformUsers.Persistence.Context;
using MassTransit;
using System.Reflection;

namespace HC.PlatformUsers.Jobs;

internal class ProcessOutboxMessagesJob(
    PlatformUsersContext context,
    IPublishEndpoint publisher) : BaseProcessOutboxMessagesJob<PlatformUsersContext, Assembly>(publisher)
{
    protected override PlatformUsersContext Context { get; init; } = context;
    protected override IReadOnlyList<Assembly> MessagesAssembly { get; init; } = 
        [typeof(PlatformUserEventsAssembly).Assembly, typeof(MediaEventsAssembly).Assembly];
}
