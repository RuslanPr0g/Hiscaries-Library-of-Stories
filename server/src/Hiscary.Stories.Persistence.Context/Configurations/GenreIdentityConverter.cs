using StackNucleus.DDD.Persistence.EF.Postgres.Configurations;
using Hiscary.Stories.Domain.Genres;

namespace Hiscary.Stories.Persistence.Context.Configurations;

public class GenreIdentityConverter : IdentityConverter<GenreId>
{
    public GenreIdentityConverter() :
        base((x) => new GenreId(x))
    {
    }
}
