using HC.Application.ResultModels.Response;
using HC.Application.Stories.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Stories.Command;

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