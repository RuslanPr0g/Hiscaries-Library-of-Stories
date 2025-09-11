using Hiscary.Stories.Persistence.Context;
using StackNucleus.DDD.Outbox.Jobs;

namespace Hiscary.Stories.Jobs;

internal class CleanOutboxJob(StoriesContext context) : BaseCleanOutboxJob<StoriesContext>
{
    protected override StoriesContext Context { get; init; } = context;
}
