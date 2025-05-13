using HC.Notifications.Application.Write.Notifications.Services;

namespace HC.Notifications.Application.Write.Notifications.Command.BecomePublisher;

public class ReadNotificationCommandHandler : IRequestHandler<ReadNotificationCommand, OperationResult>
{
    private readonly INotificationWriteService _service;

    public ReadNotificationCommandHandler(INotificationWriteService userService)
    {
        _service = userService;
    }

    public async Task<OperationResult> Handle(ReadNotificationCommand request, CancellationToken cancellationToken)
    {
        await _service.ReadNotifications(request);
        return OperationResult.CreateSuccess();
    }
}