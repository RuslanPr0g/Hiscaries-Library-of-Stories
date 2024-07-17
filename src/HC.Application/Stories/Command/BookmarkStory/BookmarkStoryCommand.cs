using HC.Application.Models.Connection;
using HC.Application.Models.Response;
using MediatR;

namespace HC.Application.Stories.Command;

public class BookmarkStoryCommand : IRequest<PublishStoryResult>
{
    public UserConnection User { get; set; }
    public int UserId { get; set; }
    public int StoryId { get; set; }
}