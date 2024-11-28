using HC.API.Extensions;
using HC.API.Requests.Libraries;
using HC.Application.Read.Users.Queries;
using HC.Application.Write.PlatformUsers.Command.BecomePublisher;
using HC.Application.Write.PlatformUsers.Command.PublishReview;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
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

        group.MapGet("/libraries", GetLibrary)
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPut("/libraries", EditLibrary)
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/libraries/subscriptions/subscribe", SubscribeToLibrary)
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/libraries/subscriptions/unsubscribe", UnsubscribeFromLibrary)
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

        return await mediator.SendMessageGetResult(command);
    }

    private static async Task<IResult> GetLibrary([FromQuery] Guid? libraryId, HttpContext context, [FromServices] IMediator mediator)
    {
        var userIdClaim = context.User.GetUserId();
        if (userIdClaim is null)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var query = new GetLibraryInfoQuery { UserId = userIdClaim.Value, LibraryId = libraryId };
        var result = await mediator.Send(query);
        return result.ToResult();
    }

    private static async Task<IResult> EditLibrary([FromBody] EditLibraryRequest request, HttpContext context, [FromServices] IMediator mediator)
    {
        var userIdClaim = context.User.GetUserId();
        if (userIdClaim is null)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var isImageAlreadyUrl = request.Avatar.ImageStringToBytes();

        var query = new EditLibraryCommand
        {
            UserId = userIdClaim.Value,
            LibraryId = request.LibraryId,
            Avatar = isImageAlreadyUrl.Image,
            ShouldUpdateImage = isImageAlreadyUrl.IsUpdated,
            Bio = request.Bio,
            LinksToSocialMedia = request.LinksToSocialMedia,
        };
        var result = await mediator.Send(query);
        return result.ToResult();
    }

    private static async Task<IResult> SubscribeToLibrary([FromBody] LibrarySubscriptionRequest request, HttpContext context, [FromServices] IMediator mediator)
    {
        var userIdClaim = context.User.GetUserId();
        if (userIdClaim is null)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var query = new SubscribeToLibraryCommand
        {
            UserId = userIdClaim.Value,
            LibraryId = request.LibraryId,
        };

        var result = await mediator.Send(query);
        return result.ToResult();
    }

    private static async Task<IResult> UnsubscribeFromLibrary([FromBody] LibrarySubscriptionRequest request, HttpContext context, [FromServices] IMediator mediator)
    {
        var userIdClaim = context.User.GetUserId();
        if (userIdClaim is null)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var query = new UnsubscribeFromLibraryCommand
        {
            UserId = userIdClaim.Value,
            LibraryId = request.LibraryId,
        };

        var result = await mediator.Send(query);
        return result.ToResult();
    }
}
