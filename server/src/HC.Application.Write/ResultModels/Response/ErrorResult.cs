using System.Collections.Generic;
using HC.Application.Write.ResultModels.Message;

namespace HC.Application.Write.ResultModels.Response;

public record ErrorResult(List<ErrorMessage> Errors);