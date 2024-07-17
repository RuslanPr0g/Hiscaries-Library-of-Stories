using HC.Application.Models.Connection;
using MediatR;

namespace HC.Application.Stories.Command;

public class DeleteCommentCommand : IRequest<int>
{
    public UserConnection User { get; set; }
    public int Id { get; set; }
}