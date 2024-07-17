using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HC.Application.Interface;
using HC.Application.Models.Response;
using HC.Domain.Story;
using MediatR;

namespace HC.Application.Stories.Command.UpdateStory;

public class UpdateStoryCommandHandler : IRequestHandler<UpdateStoryCommand, PublishStoryResult>
{
    private readonly IStoryWriteService _storyService;

    public UpdateStoryCommandHandler(IStoryWriteService storyService)
    {
        _storyService = storyService;
    }

    public async Task<PublishStoryResult> Handle(UpdateStoryCommand request, CancellationToken cancellationToken)
    {
        Story story = await _storyService.GetStoryById(request.StoryId, request.User);

        if (story is null) return new PublishStoryResult(ResultStatus.Fail, "There is no such story!", 0);

        if (request.Title != "")
            story.Title = request.Title;
        if (request.Description != "")
            story.Description = request.Description;
        if (request.AuthorName != "")
            story.AuthorName = request.AuthorName;
        if (request.GenreIds.Any())
            story.GenreIds = request.GenreIds;
        if (request.AgeLimit >= 0 && request.AgeLimit <= 100)
            story.AgeLimit = request.AgeLimit;
        if (request.ImagePreview != null)
            story.ImagePreview = request.ImagePreview;

        return await _storyService.UpdateStory(story, request.User);
    }
}