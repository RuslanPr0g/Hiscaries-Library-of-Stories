using System.Collections.Generic;
using HC.Application.Models.Connection;
using HC.Domain.Story.Comment;
using MediatR;

namespace HC.Application.Stories.Query;

public class GetAllCommentsQuery : IRequest<List<Comment>>
{
    public UserConnection User { get; set; }
}