using HC.Application.Write.PlatformUsers.Services;
using HC.Application.Write.ResultModels.Response;
using MediatR;

namespace HC.Application.Write.Stories.Command;

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