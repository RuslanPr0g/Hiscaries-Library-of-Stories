using StackNucleus.DDD.Persistence.EF.Postgres.Extensions;
using Hiscary.Stories.Domain.Genres;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hiscary.Stories.Persistence.Context.Configurations;

public class GenreConfigurations : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("Genres");
        builder.ConfigureEntity<Genre, GenreId, GenreIdentityConverter>();
    }
}
