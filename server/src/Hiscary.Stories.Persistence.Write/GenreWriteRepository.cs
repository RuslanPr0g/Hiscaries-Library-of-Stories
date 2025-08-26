using Hiscary.Stories.Domain.DataAccess;
using Hiscary.Stories.Domain.Genres;
using Hiscary.Stories.Persistence.Context;
using StackNucleus.DDD.Persistence.EF.Postgres;

namespace Hiscary.Stories.Persistence.Write;

public class GenreWriteRepository(StoriesContext context)
    : BaseWriteRepository<Genre, GenreId, StoriesContext>,
    IGenreWriteRepository
{
    protected override StoriesContext Context { get; init; } = context;
}
