using HC.Application.Models.Connection;
using HC.Domain.Story.Comment;
using MediatR;

namespace HC.Application.Stories.Command;

public class AddCommentCommand : IRequest<int>
{
    public UserConnection User { get; set; }
    public Comment Comment { get; set; }
}