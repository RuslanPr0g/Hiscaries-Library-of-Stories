using HC.Application.ResultModels.Response;
using HC.Application.Stories.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Stories.Command;

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