using StackNucleus.DDD.Persistence.EF.Postgres;
using Hiscary.UserAccounts.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Hiscary.UserAccounts.Persistence.Context;

public sealed class UserAccountsContext(DbContextOptions<UserAccountsContext> options)
    : BaseNucleusContext<UserAccountsContext>(options)
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
