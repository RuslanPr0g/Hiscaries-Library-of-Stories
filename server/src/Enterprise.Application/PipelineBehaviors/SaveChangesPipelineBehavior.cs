using Microsoft.Extensions.Logging;

public class SaveChangesPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<SaveChangesPipelineBehavior<TRequest, TResponse>> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public SaveChangesPipelineBehavior(
        ILogger<SaveChangesPipelineBehavior<TRequest, TResponse>> logger,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling {typeof(TRequest).Name}");

        var response = await next();

        // TODO: make it less dumb
        if (typeof(TRequest).Name.EndsWith("Command"))
        {
            await _unitOfWork.SaveChanges();
        }

        _logger.LogInformation($"Handled {typeof(TResponse).Name}");
        return response;
    }
}