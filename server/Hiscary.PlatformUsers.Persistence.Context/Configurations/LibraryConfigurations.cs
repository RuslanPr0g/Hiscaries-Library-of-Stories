using StackNucleus.DDD.Persistence.EF.Postgres.Extensions;
using Hiscary.PlatformUsers.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hiscary.PlatformUsers.Persistence.Context.Configurations;

public class LibraryConfigurations : IEntityTypeConfiguration<Library>
{
    public void Configure(EntityTypeBuilder<Library> builder)
    {
        builder.ToTable("Libraries");
        builder.ConfigureEntity<Library, LibraryId, LibraryIdentityConverter>();
    }
}
