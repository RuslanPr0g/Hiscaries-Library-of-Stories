using Hiscary.UserAccounts.Persistence.Context;
using StackNucleus.DDD.Outbox.Jobs;

namespace Hiscary.UserAccounts.Jobs;

internal class CleanOutboxJob(UserAccountsContext context) : BaseCleanOutboxJob<UserAccountsContext>
{
    protected override UserAccountsContext Context { get; init; } = context;
}
