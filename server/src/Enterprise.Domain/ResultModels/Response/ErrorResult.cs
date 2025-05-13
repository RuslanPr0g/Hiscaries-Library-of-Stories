using Enterprise.Domain.ResultModels;

namespace Enterprise.Application.ResultModels.Response;

public record ErrorResult(List<ErrorMessage> Errors);