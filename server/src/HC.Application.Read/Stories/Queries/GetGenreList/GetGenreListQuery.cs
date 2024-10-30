using HC.Application.Read.Genres.ReadModels;
using MediatR;

namespace HC.Application.Read.Stories.Queries;

public sealed class GetGenreListQuery : IRequest<IEnumerable<GenreReadModel>>
{
}