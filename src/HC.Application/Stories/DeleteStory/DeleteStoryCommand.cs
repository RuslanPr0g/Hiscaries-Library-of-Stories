using HC.Application.Models.Connection;
using MediatR;

namespace HC.Application.Stories.DeleteStory;

public class DeleteStoryCommand : IRequest<int>
{
    public UserConnection User { get; set; }
    public int StoryId { get; set; }
}