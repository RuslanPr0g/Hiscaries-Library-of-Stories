using HC.Application.Constants;
using HC.Application.Write.PlatformUsers.Services;
using HC.Application.Write.ResultModels.Response;
using MediatR;

namespace HC.Application.Write.PlatformUsers.Command.BecomePublisher;

public class BecomePublisherCommandHandler : IRequestHandler<BecomePublisherCommand, OperationResult>
{
    private readonly IUserWriteService _userService;

    public BecomePublisherCommandHandler(IUserWriteService userService)
    {
        _userService = userService;
    }

    public async Task<OperationResult> Handle(BecomePublisherCommand request, CancellationToken cancellationToken)
    {
        if (request.Username is null)
        {
            return OperationResult.CreateClientSideError(UserFriendlyMessages.UsernameEmpty);
        }

        await _userService.BecomePublisher(request.Username);
        return OperationResult.CreateSuccess();
    }
}