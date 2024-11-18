using HC.API.Extensions;
using HC.Application.Write.PlatformUsers.Command.BecomePublisher;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace HC.API.ApiEndpoints;

public static class PlatformUserEndpoints
{
    public static void MapPlatformUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/users")
            .WithTags("Users");

        group.MapPost("/become-publisher", BecomePublisher)
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }

    private static async Task<IResult> BecomePublisher(HttpContext context, [FromServices] IMediator mediator)
    {
        var userIdClaim = context.User.GetUserId();
        if (userIdClaim is null)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var command = new BecomePublisherCommand { Id = userIdClaim.Value };
        var result = await mediator.Send(command);
        return result.ToResult();
    }
}
