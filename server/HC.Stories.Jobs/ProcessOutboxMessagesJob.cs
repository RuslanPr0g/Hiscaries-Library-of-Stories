using Enterprise.Domain.EventPublishers;
using Enterprise.Outbox;
using HC.Media.IntegrationEvents;
using HC.Stories.IntegrationEvents;
using HC.Stories.Persistence.Context;
using System.Reflection;

namespace HC.Stories.Jobs;

internal class ProcessOutboxMessagesJob(
    StoriesContext context,
    IEventPublisher publisher) : BaseProcessOutboxMessagesJob<StoriesContext, Assembly>(publisher)
{
    protected override StoriesContext Context { get; init; } = context;
    protected override IReadOnlyList<Assembly> MessagesAssembly { get; init; } =
        [
        typeof(StoryIntegrationEventsAssembly).Assembly,
        typeof(MediaIntegrationEventsAssembly).Assembly];
}
