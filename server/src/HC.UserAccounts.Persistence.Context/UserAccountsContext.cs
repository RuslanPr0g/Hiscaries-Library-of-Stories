using Enterprise.Persistence.Context;
using HC.UserAccounts.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HC.UserAccounts.Persistence.Context;

public sealed class UserAccountsContext(DbContextOptions<UserAccountsContext> options)
    : BaseEnterpriseContext<UserAccountsContext>(options)
{
    public override string SchemaName => "useraccounts";

    public DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
