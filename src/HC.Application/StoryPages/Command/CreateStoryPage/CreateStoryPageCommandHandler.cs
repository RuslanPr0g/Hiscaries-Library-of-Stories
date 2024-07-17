using System.Threading;
using System.Threading.Tasks;
using HC.Application.Interface;
using HC.Application.Models.Response;
using HC.Domain.Story;
using MediatR;

namespace HC.Application.StoryPages.Command;

public class CreateStoryPageCommandHandler : IRequestHandler<CreateStoryPageCommand, AddStoryPageResult>
{
    private readonly IStoryPageService _storyService;

    public CreateStoryPageCommandHandler(IStoryPageService storyPageService)
    {
        _storyService = storyPageService;
    }

    public async Task<AddStoryPageResult> Handle(CreateStoryPageCommand request, CancellationToken cancellationToken)
    {
        StoryPage storyPage = new StoryPage
        {
            Story = new Story { Id = request.StoryId },
            Page = request.Page,
            Content = request.Content
        };

        return await _storyService.AddPageForStory(storyPage, request.User);
    }
}