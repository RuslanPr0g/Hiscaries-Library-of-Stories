using Hiscary.Outbox;
using Hiscary.PlatformUsers.Persistence.Context;

namespace Hiscary.PlatformUsers.Jobs;

internal class CleanOutboxJob(PlatformUsersContext context) : BaseCleanOutboxJob<PlatformUsersContext>
{
    protected override PlatformUsersContext Context { get; init; } = context;
}
