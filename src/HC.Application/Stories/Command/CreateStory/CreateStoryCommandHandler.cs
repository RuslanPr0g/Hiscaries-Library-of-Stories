using System.Threading;
using System.Threading.Tasks;
using HC.Application.Interface;
using HC.Application.Models.Response;
using HC.Domain.Story;
using HC.Domain.User;
using MediatR;

namespace HC.Application.Stories.Command;

public class CreateStoryCommandHandler : IRequestHandler<CreateStoryCommand, PublishStoryResult>
{
    private readonly IStoryService _storyService;
    private readonly IUserService _userService;

    public CreateStoryCommandHandler(IStoryService storyService, IUserService userService)
    {
        _storyService = storyService;
        _userService = userService;
    }

    public async Task<PublishStoryResult> Handle(CreateStoryCommand request, CancellationToken cancellationToken)
    {
        User publisher = await _userService.GetUserByUsername(request.User.Username, request.User);

        Story story = new Story
        {
            Publisher = publisher,
            Title = request.Title,
            Description = request.Description,
            AuthorName = request.AuthorName,
            GenreIds = request.GenreIds,
            AgeLimit = request.AgeLimit,
            ImagePreview = request.ImagePreview,
            DatePublished = request.DatePublished,
            DateWritten = request.DateWritten
        };

        return await _storyService.PublishStory(story, request.User);
    }
}