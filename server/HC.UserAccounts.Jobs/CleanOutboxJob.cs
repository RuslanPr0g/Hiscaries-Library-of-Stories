using Enterprise.Outbox;
using HC.UserAccounts.Persistence.Context;

namespace HC.UserAccounts.Jobs;

internal class CleanOutboxJob(UserAccountsContext context) : BaseCleanOutboxJob<UserAccountsContext>
{
    protected override UserAccountsContext Context { get; init; } = context;
}
