using System;
using System.Threading;
using System.Threading.Tasks;
using HC.Application.Interface;
using HC.Application.Models.Response;
using HC.Domain.Story;
using MediatR;

namespace HC.Application.Stories.Command.BookmarkStory;

internal class BookmarkStoryCommandHandler : IRequestHandler<BookmarkStoryCommand, PublishStoryResult>
{
    private readonly IStoryService _storyService;
    private readonly IUserService _userService;

    public BookmarkStoryCommandHandler(IStoryService storyService, IUserService userService)
    {
        _storyService = storyService;
        _userService = userService;
    }

    public async Task<PublishStoryResult> Handle(BookmarkStoryCommand request, CancellationToken cancellationToken)
    {
        StoryBookMark bookMark = new StoryBookMark
        {
            User = new User
            {
                Id = request.UserId
            },
            Story = new Story
            {
                Id = request.StoryId
            },
            DateAdded = DateTime.Now
        };

        return await _storyService.BookmarkStory(bookMark, request.User);
    }
}