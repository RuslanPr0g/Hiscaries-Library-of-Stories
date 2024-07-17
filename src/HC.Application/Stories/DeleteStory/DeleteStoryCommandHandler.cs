using System.Threading;
using System.Threading.Tasks;
using HC.Application.Interface;

using MediatR;

namespace HC.Application.Stories.DeleteStory;

public class DeleteStoryCommandHandler : IRequestHandler<DeleteStoryCommand, int>
{
    private readonly IStoryService _storyService;

    public DeleteStoryCommandHandler(IStoryService storyService)
    {
        _storyService = storyService;
    }

    public async Task<int> Handle(DeleteStoryCommand request, CancellationToken cancellationToken)
    {
        return await _storyService.DeleteStory(new Story { Id = request.StoryId }, request.User);
    }
}