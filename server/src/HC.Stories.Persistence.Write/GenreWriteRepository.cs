using HC.Stories.Domain.DataAccess;
using HC.Stories.Domain.Genres;
using HC.Stories.Persistence.Context;

namespace HC.Stories.Persistence.Write;

public class GenreWriteRepository(StoriesContext context)
    : BaseWriteRepository<Genre, GenreId, StoriesContext>,
    IGenreWriteRepository
{
    protected override StoriesContext Context { get; init; } = context;
}
