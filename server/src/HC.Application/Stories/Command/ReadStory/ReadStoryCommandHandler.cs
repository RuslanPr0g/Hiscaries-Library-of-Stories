using HC.Application.Models.Response;
using HC.Application.Services.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Stories.Command.ReadStory;

internal class ReadStoryCommandHandler : IRequestHandler<ReadStoryCommand, OperationResult>
{
    private readonly IUserWriteService _service;

    public ReadStoryCommandHandler(IUserWriteService storyService)
    {
        _service = storyService;
    }

    public async Task<OperationResult> Handle(ReadStoryCommand request, CancellationToken cancellationToken)
    {
        return await _service.ReadStoryHistory(request);
    }
}