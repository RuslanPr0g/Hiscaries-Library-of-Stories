using HC.Domain.PlatformUsers;
using HC.Persistence.Context.Configurations.Converters;
using HC.Persistence.Context.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HC.Persistence.Context.Configurations;

public class LibraryConfigurations : IEntityTypeConfiguration<Library>
{
    public void Configure(EntityTypeBuilder<Library> builder)
    {
        builder.ConfigureEntity<Library, LibraryId, LibraryIdentityConverter>();
    }
}
