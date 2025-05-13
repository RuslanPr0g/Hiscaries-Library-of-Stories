using HC.Application.Write.Stories.Services;

namespace HC.Application.Write.Stories.Command;

public sealed class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, OperationResult>
{
    private readonly IStoryWriteService _service;

    public DeleteGenreCommandHandler(IStoryWriteService userService)
    {
        _service = userService;
    }

    public async Task<OperationResult> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        return await _service.DeleteGenre(request);
    }
}