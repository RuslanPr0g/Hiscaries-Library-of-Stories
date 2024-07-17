using System.Collections.Generic;
using HC.Application.Models.Connection;
using HC.Domain.Story;
using MediatR;

namespace HC.Application.Stories.Query.Bookmarks;

public class BookmarkStoryQuery : IRequest<List<Story>>
{
    public UserConnection User { get; set; }
    public int UserId { get; set; }
}