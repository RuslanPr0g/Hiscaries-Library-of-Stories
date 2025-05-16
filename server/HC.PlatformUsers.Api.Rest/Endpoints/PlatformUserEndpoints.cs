using Enterprise.Api.Rest;
using Enterprise.Domain.Extensions;
using Enterprise.Domain.ResultModels.Response;
using HC.PlatformUsers.Api.Rest.Requests;
using HC.PlatformUsers.Api.Rest.Requests.Libraries;
using HC.PlatformUsers.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace HC.PlatformUsers.Api.Rest.Endpoints;

public static class PlatformUserEndpoints
{
    public static void MapPlatformUsersEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/users")
            .WithTags("Users");

        group.MapPost("/healthcheck", () =>
        {
            return Results.Ok("PLATFORM USER SERVICE WORKS!");
        })
            .AllowAnonymous()
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

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

        group.MapPost("/read", ReadStory)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/bookmark", BookmarkStory)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }

    private static async Task<IResult> BookmarkStory(
        [FromBody] BookmarkStoryRequest request,
        [FromServices] IPlatformUserWriteService service,
        HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.GetUserId();
        if (!userIdClaim.HasValue)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var result = await service.BookmarkStory(userIdClaim.Value, request.StoryId);

        return result.ToResult();
    }


    private static async Task<IResult> ReadStory(
        [FromBody] ReadStoryRequest request,
        [FromServices] IPlatformUserWriteService service,
        HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.GetUserId();
        if (!userIdClaim.HasValue)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var result = await service.ReadStoryPage(userIdClaim.Value, request.StoryId, request.PageRead);

        return result.ToResult();
    }

    private static async Task<IResult> BecomePublisher(
        HttpContext context,
        [FromServices] IPlatformUserWriteService service)
    {
        var userIdClaim = context.User.GetUserId();
        if (!userIdClaim.HasValue)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var result = await service.BecomePublisher(userIdClaim.Value);

        return result.ToResult();
    }

    private static async Task<IResult> GetLibrary(
        [FromQuery] Guid? libraryId,
        HttpContext context,
        [FromServices] IPlatformUserReadService service)
    {
        var userIdClaim = context.User.GetUserId();
        if (!userIdClaim.HasValue)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var result = await service.GetLibraryInformation(userIdClaim.Value, libraryId);

        return result.ToResult();
    }

    private static async Task<IResult> EditLibrary(
        [FromBody] EditLibraryRequest request,
        HttpContext context,
        [FromServices] IPlatformUserWriteService service)
    {
        var userIdClaim = context.User.GetUserId();
        if (!userIdClaim.HasValue)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var (Image, IsUpdated) = request.Avatar.ImageStringToBytes();

        var result = await service.EditLibraryInfo(
            userIdClaim.Value,
            request.LibraryId,
            request.Bio,
            Image,
            IsUpdated,
            request.LinksToSocialMedia);

        return result.ToResult();
    }

    private static async Task<IResult> SubscribeToLibrary(
        [FromBody] LibrarySubscriptionRequest request,
        HttpContext context,
        [FromServices] IPlatformUserWriteService service)
    {
        var userIdClaim = context.User.GetUserId();
        if (!userIdClaim.HasValue)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var result = await service.SubscribeToLibrary(
            userIdClaim.Value,
            request.LibraryId);

        return result.ToResult();
    }

    private static async Task<IResult> UnsubscribeFromLibrary(
        [FromBody] LibrarySubscriptionRequest request,
        HttpContext context,
        [FromServices] IPlatformUserWriteService service)
    {
        var userIdClaim = context.User.GetUserId();
        if (!userIdClaim.HasValue)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var result = await service.UnsubscribeFromLibrary(
            userIdClaim.Value,
            request.LibraryId);

        return result.ToResult();
    }
}
