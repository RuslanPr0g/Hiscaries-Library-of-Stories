using Enterprise.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HC.UserAccounts.Persistence.Context;

public class UserAccountsDatabaseDesignTimeDbContextFactory
    : EnterpriseDatabaseDesignTimeDbContextFactory<UserAccountsContext>
{
    public override UserAccountsContext CreateDbContextBasedOnOptions(
        DbContextOptions<UserAccountsContext> options)
    {
        return new UserAccountsContext(options);
    }
}
