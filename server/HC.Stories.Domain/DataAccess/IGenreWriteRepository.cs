using Enterprise.Domain.DataAccess;
using HC.Stories.Domain.Genres;

namespace HC.Stories.Domain.DataAccess;

public interface IGenreWriteRepository : IBaseWriteRepository<Genre, GenreId>
{
}