using HC.Application.Models.Connection;
using HC.Application.Models.Response;
using MediatR;

namespace HC.Application.StoryPages.Command;

public class CreateStoryPageCommand : IRequest<AddStoryPageResult>
{
    public UserConnection User { get; set; }
    public int StoryId { get; set; }

    public int Page { get; set; }

    public string Content { get; set; }
}