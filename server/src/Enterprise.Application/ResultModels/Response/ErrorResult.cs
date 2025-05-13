using Enterprise.Application.ResultModels.Message;

namespace Enterprise.Application.ResultModels.Response;

public record ErrorResult(List<ErrorMessage> Errors);