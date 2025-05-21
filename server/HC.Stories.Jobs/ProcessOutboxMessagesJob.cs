using Enterprise.Outbox;
using HC.Media.Domain.Events;
using HC.Stories.Domain.Events;
using HC.Stories.Persistence.Context;
using MassTransit;
using System.Reflection;

namespace HC.Stories.Jobs;

internal class ProcessOutboxMessagesJob(
    StoriesContext context,
    IPublishEndpoint publisher) : BaseProcessOutboxMessagesJob<StoriesContext, Assembly>(publisher)
{
    protected override StoriesContext Context { get; init; } = context;
    protected override IReadOnlyList<Assembly> MessagesAssembly { get; init; } =
        [typeof(StoryEventsAssembly).Assembly, typeof(MediaEventsAssembly).Assembly];
}
