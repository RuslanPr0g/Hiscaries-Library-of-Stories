using Hiscary.PlatformUsers.Persistence.Context;
using StackNucleus.DDD.Outbox.Jobs;

namespace Hiscary.PlatformUsers.Jobs;

internal class CleanOutboxJob(PlatformUsersContext context) : BaseCleanOutboxJob<PlatformUsersContext>
{
    protected override PlatformUsersContext Context { get; init; } = context;
}
