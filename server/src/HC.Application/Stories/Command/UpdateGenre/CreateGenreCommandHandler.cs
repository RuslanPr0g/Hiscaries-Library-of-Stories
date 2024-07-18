using HC.Application.Interface;
using HC.Application.Models.Response;
using HC.Application.Stories.Command;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Users.Command;

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