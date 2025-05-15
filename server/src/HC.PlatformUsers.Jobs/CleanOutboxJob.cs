using Enterprise.Outbox;
using HC.PlatformUsers.Persistence.Context;

namespace HC.PlatformUsers.Jobs;

internal class CleanOutboxJob(PlatformUsersContext context) : BaseCleanOutboxJob<PlatformUsersContext>
{
    protected override PlatformUsersContext Context { get; init; } = context;
}
