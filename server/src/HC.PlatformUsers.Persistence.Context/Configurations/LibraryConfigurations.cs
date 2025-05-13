using HC.Persistence.Context.Configurations.Converters;

namespace HC.Persistence.Context.Configurations;

public class LibraryConfigurations : IEntityTypeConfiguration<Library>
{
    public void Configure(EntityTypeBuilder<Library> builder)
    {
        builder.ToTable("Libraries");
        builder.ConfigureEntity<Library, LibraryId, LibraryIdentityConverter>();
    }
}
