using Hiscary.PlatformUsers.Persistence.Context;
using Hiscary.Shared.Outbox;

namespace Hiscary.PlatformUsers.Jobs;

internal class CleanOutboxJob(PlatformUsersContext context) : BaseCleanOutboxJob<PlatformUsersContext>
{
    protected override PlatformUsersContext Context { get; init; } = context;
}
