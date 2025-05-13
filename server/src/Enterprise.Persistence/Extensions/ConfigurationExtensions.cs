using HC.Domain;
using HC.Persistence.Context.Configurations.Converters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace HC.Persistence.Context.Extensions;

public static class ConfigurationExtensions
{
    public static EntityTypeBuilder<TEntity> ConfigureEntity<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : Entity
    {
        builder.Property(u => u.Version).IsRequired();
        builder.Property(u => u.CreatedAt).IsRequired();
        builder.Property(u => u.EditedAt).IsRequired();
        return builder;
    }

    public static EntityTypeBuilder<TEntity> ConfigureEntity<TEntity, TIdentity, TIdentityConverter>(
            this EntityTypeBuilder<TEntity> builder,
            TIdentityConverter? converter = null)
            where TEntity : Entity<TIdentity>
            where TIdentity : Identity
            where TIdentityConverter : IdentityConverter<TIdentity>, new()
    {
        builder.HasKey(u => u.Id);
        if (converter is null)
        {
            builder.Property(u => u.Id).HasConversion(new TIdentityConverter()).HasValueGenerator<GuidValueGenerator>();
        }
        else
        {
            builder.Property(u => u.Id).HasConversion(converter).HasValueGenerator<GuidValueGenerator>();
        }
        builder.ConfigureEntity();
        return builder;
    }
}
