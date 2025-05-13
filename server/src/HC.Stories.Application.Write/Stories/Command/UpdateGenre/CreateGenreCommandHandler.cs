using HC.Application.Write.Stories.Services;

namespace HC.Application.Write.Stories.Command;

public sealed class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, OperationResult>
{
    private readonly IStoryWriteService _service;

    public UpdateGenreCommandHandler(IStoryWriteService userService)
    {
        _service = userService;
    }

    public async Task<OperationResult> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        return await _service.UpdateGenre(request);
    }
}