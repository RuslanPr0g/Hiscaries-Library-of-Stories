using HC.Application.Genres.ReadModels;
using MediatR;
using System.Collections.Generic;

namespace HC.Application.Stories.Query;

public sealed class GetGenreListQuery : IRequest<IEnumerable<GenreReadModel>>
{
}