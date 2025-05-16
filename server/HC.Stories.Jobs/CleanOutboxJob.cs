using Enterprise.Outbox;
using HC.Stories.Persistence.Context;

namespace HC.Stories.Jobs;

internal class CleanOutboxJob(StoriesContext context) : BaseCleanOutboxJob<StoriesContext>
{
    protected override StoriesContext Context { get; init; } = context;
}
