using HC.Application.ResultModels.Response;
using HC.Application.Stories.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Stories.Command;

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