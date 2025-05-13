namespace HC.Stories.Persistence.Context.Configurations;

public class GenreConfigurations : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("Genres");
        builder.ConfigureEntity<Genre, GenreId, GenreIdentityConverter>();
    }
}
