using Hiscary.Persistence.Context;
using Hiscary.UserAccounts.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Hiscary.UserAccounts.Persistence.Context;

public sealed class UserAccountsContext(DbContextOptions<UserAccountsContext> options)
    : BaseEnterpriseContext<UserAccountsContext>(options)
{
    public static string SCHEMA_NAME = "useraccounts";

    public override string SchemaName => SCHEMA_NAME;

    public DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
