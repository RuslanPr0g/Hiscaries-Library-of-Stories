using System.Collections.Generic;
using HC.Application.ResultModels.Message;

namespace HC.Application.ResultModels.Response;

public record ErrorResult(List<ErrorMessage> Errors);