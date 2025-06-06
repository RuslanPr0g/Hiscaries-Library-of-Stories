﻿using Enterprise.Persistence.Context;
using HC.Notifications.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HC.Notifications.Persistence.Context;

public sealed class NotificationsContext(DbContextOptions<NotificationsContext> options)
    : BaseEnterpriseContext<NotificationsContext>(options)
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
