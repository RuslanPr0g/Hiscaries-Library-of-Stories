using HC.Stories.Application.Write.Stories.Services;

namespace HC.Stories.Application.Write.Stories.Services.UpdateGenre;

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