using HC.Application.Models.Connection;
using MediatR;

namespace HC.Application.Stories.Command;

public class UpdateCommentCommand : IRequest<int>
{
    public UserConnection User { get; set; }
    public int Id { get; set; }
    public int StoryId { get; set; }
    public int UserId { get; set; }
    public string Content { get; set; }
}