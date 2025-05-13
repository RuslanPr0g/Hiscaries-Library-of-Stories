using HC.Stories.Application.Read.Genres.ReadModels;

namespace HC.Stories.Application.Read.Stories.Queries.GetGenreList;

public sealed class GetGenreListQuery : IRequest<IEnumerable<GenreReadModel>>
{
}