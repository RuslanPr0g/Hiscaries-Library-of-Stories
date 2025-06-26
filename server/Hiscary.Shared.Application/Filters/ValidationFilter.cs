using StackNucleus.DDD.Domain.ResultModels.Message;
using StackNucleus.DDD.Domain.ResultModels.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hiscary.Shared.Application.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            KeyValuePair<string, IEnumerable<string>>[] errors = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

            ErrorResult errorResponse = new(new List<ErrorMessage>());

            foreach (KeyValuePair<string, IEnumerable<string>> error in errors)
                foreach (string subError in error.Value)
                {
                    ErrorMessage errorModel = new()
                    {
                        FieldName = error.Key,
                        Message = subError
                    };

                    errorResponse.Errors.Add(errorModel);
                }

            context.Result = new BadRequestObjectResult(errorResponse);
            return;
        }

        await next();
    }
}