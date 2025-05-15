using Enterprise.Domain.ResultModels.Message;

namespace Enterprise.Domain.ResultModels.Response;

public record ErrorResult(List<ErrorMessage> Errors);