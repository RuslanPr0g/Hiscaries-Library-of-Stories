using HC.Application.ResultModels.Response;
using HC.Application.Users.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Stories.Command;

internal class BookmarkStoryCommandHandler : IRequestHandler<BookmarkStoryCommand, OperationResult>
{
    private readonly IUserWriteService _userService;

    public BookmarkStoryCommandHandler(IUserWriteService userService)
    {
        _userService = userService;
    }

    public async Task<OperationResult> Handle(BookmarkStoryCommand request, CancellationToken cancellationToken)
    {
        return await _userService.BookmarkStory(request);
    }
}