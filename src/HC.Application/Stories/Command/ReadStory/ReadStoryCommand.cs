using HC.Application.Models.Connection;
using MediatR;

namespace HC.Application.Stories.Command.ReadStory;

public class ReadStoryCommand : IRequest<int>
{
    public UserConnection User { get; set; }
    public int StoryId { get; set; }
    public int UserId { get; set; }
    public int Page { get; set; }
}