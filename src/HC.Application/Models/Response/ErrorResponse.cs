using System.Collections.Generic;
using HC.Application.Models.Message;

namespace HC.Application.Models.Response;

public record ErrorResponse(List<ErrorMessage> Errors);