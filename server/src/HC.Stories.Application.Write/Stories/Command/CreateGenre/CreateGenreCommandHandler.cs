using HC.Application.Write.Stories.Services;

namespace HC.Application.Write.Stories.Command;

public sealed class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, OperationResult>
{
    private readonly IStoryWriteService _service;

    public CreateGenreCommandHandler(IStoryWriteService userService)
    {
        _service = userService;
    }

    public async Task<OperationResult> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        return await _service.CreateGenre(request);
    }
}