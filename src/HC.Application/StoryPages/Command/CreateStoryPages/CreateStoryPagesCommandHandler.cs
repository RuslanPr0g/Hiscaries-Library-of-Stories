using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HC.Application.Interface;
using HC.Application.Models.Response;
using HC.Domain.Story;
using MediatR;

namespace HC.Application.StoryPages.Command.CreateStoryPages;

public class CreateStoryPagesCommandHandler : IRequestHandler<CreateStoryPagesCommand, AddStoryPageResult>
{
    private readonly IStoryPageService _storyService;

    public CreateStoryPagesCommandHandler(IStoryPageService storyPageService)
    {
        _storyService = storyPageService;
    }

    public async Task<AddStoryPageResult> Handle(CreateStoryPagesCommand request, CancellationToken cancellationToken)
    {
        AddStoryPageResult lastResult = new AddStoryPageResult(ResultStatus.Fail, "Unknown error occcured", -1);
        await _storyService.RemoveStoryPages(request.StoryId, request.User);

        for (int i = 0; i < request.Content.Count; i++)
        {
            StoryPage page = new StoryPage
            {
                Story = new Story { Id = request.StoryId },
                Page = i + 1,
                Content = request.Content.ElementAt(i)
            };

            lastResult = await _storyService.AddPageForStory(page, request.User);
        }

        return lastResult;
    }
}