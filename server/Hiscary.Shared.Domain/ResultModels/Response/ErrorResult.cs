using Hiscary.Shared.Domain.ResultModels.Message;

namespace Hiscary.Shared.Domain.ResultModels.Response;

public record ErrorResult(List<ErrorMessage> Errors);