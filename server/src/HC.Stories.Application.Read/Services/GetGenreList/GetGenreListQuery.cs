using HC.Stories.Application.Read.Genres.ReadModels;

namespace HC.Stories.Application.Read.Services.GetGenreList;

public sealed class GetGenreListQuery : IRequest<IEnumerable<GenreReadModel>>
{
}