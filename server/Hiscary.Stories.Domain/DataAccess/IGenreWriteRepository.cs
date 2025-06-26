using Hiscary.Domain.DataAccess;
using Hiscary.Stories.Domain.Genres;

namespace Hiscary.Stories.Domain.DataAccess;

public interface IGenreWriteRepository : IBaseWriteRepository<Genre, GenreId>
{
}