namespace HC.Application.Write.Stories.Command;

internal class BookmarkStoryCommandHandler : IRequestHandler<BookmarkStoryCommand, OperationResult>
{
    private readonly IPlatformUserWriteService _userService;

    public BookmarkStoryCommandHandler(IPlatformUserWriteService userService)
    {
        _userService = userService;
    }

    public async Task<OperationResult> Handle(BookmarkStoryCommand request, CancellationToken cancellationToken)
    {
        return await _userService.BookmarkStory(request);
    }
}