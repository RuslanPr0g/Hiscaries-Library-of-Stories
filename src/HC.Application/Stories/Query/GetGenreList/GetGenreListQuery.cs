using System.Collections.Generic;
using HC.Application.Models.Connection;

using MediatR;

namespace HC.Application.Stories.Query.GetGenreList;

public class GetGenreListQuery : IRequest<List<Genre>>
{
    public UserConnection User { get; set; }
}