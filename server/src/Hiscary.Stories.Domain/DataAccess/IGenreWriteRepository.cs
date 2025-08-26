using StackNucleus.DDD.Domain.Repositories;
using Hiscary.Stories.Domain.Genres;

namespace Hiscary.Stories.Domain.DataAccess;

public interface IGenreWriteRepository : IBaseWriteRepository<Genre, GenreId>
{
}