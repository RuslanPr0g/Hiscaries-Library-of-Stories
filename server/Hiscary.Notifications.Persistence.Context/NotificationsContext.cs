using Hiscary.Notifications.Domain;
using Microsoft.EntityFrameworkCore;
using StackNucleus.DDD.Persistence.EF.Postgres;
using System.Reflection;

namespace Hiscary.Notifications.Persistence.Context;

public sealed class NotificationsContext(DbContextOptions<NotificationsContext> options)
    : BaseNucleusContext<NotificationsContext>(options)
{
    public static string SCHEMA_NAME = "notifications";

    public override string SchemaName => SCHEMA_NAME;

    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
