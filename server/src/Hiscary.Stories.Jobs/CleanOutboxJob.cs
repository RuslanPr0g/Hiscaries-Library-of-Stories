using Hiscary.Shared.Outbox;
using Hiscary.Stories.Persistence.Context;

namespace Hiscary.Stories.Jobs;

internal class CleanOutboxJob(StoriesContext context) : BaseCleanOutboxJob<StoriesContext>
{
    protected override StoriesContext Context { get; init; } = context;
}
