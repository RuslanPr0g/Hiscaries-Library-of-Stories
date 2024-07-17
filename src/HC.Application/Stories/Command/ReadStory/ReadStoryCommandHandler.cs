using System;
using System.Threading;
using System.Threading.Tasks;
using HC.Application.Interface;
using HC.Domain.Story;
using HC.Domain.User;
using MediatR;

namespace HC.Application.Stories.Command.ReadStory;

internal class ReadStoryCommandHandler : IRequestHandler<ReadStoryCommand, int>
{
    private readonly IStoryWriteService _storyService;
    private readonly IUserWriteService _userService;

    public ReadStoryCommandHandler(IStoryWriteService storyService, IUserWriteService userService)
    {
        _storyService = storyService;
        _userService = userService;
    }

    public async Task<int> Handle(ReadStoryCommand request, CancellationToken cancellationToken)
    {
        User publisher = await _userService.GetUserByUsername(request.User.Username, request.User);

        StoryReadHistory story = new StoryReadHistory
        {
            User = new User
            {
                Id = request.UserId
            },
            Story = new Story
            {
                Id = request.StoryId
            },
            DateRead = DateTime.Now,
            PageRead = request.Page,
            SoftDeleted = false
        };

        return await _storyService.ReadStoryHistory(story, request.User);
    }
}