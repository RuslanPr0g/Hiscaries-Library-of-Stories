using HC.Application.Read.Genres.ReadModels;
using MediatR;
using System.Collections.Generic;

namespace HC.Application.Read.Stories.Queries;

public sealed class GetGenreListQuery : IRequest<IEnumerable<GenreReadModel>>
{
}