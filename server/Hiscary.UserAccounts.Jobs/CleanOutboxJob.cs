using Hiscary.Outbox;
using Hiscary.UserAccounts.Persistence.Context;

namespace Hiscary.UserAccounts.Jobs;

internal class CleanOutboxJob(UserAccountsContext context) : BaseCleanOutboxJob<UserAccountsContext>
{
    protected override UserAccountsContext Context { get; init; } = context;
}
