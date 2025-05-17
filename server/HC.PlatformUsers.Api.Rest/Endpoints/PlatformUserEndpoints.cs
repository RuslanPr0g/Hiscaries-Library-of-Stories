using Enterprise.Api.Rest;
using Enterprise.Domain.Extensions;
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
        IAuthorizedEndpointHandler endpointHandler,
        [FromServices] IPlatformUserWriteService service) =>
        await endpointHandler.WithUser(user =>
            service.BookmarkStory(user.Id, request.StoryId));

    private static async Task<IResult> ReadStory(
        [FromBody] ReadStoryRequest request,
        IAuthorizedEndpointHandler endpointHandler,
        [FromServices] IPlatformUserWriteService service) =>
        await endpointHandler.WithUser(user =>
            service.ReadStoryPage(user.Id, request.StoryId, request.PageRead));

    private static async Task<IResult> BecomePublisher(
        IAuthorizedEndpointHandler endpointHandler,
        [FromServices] IPlatformUserWriteService service) =>
        await endpointHandler.WithUser(user =>
            service.BecomePublisher(user.Id));

    private static async Task<IResult> GetLibrary(
        [FromQuery] Guid? libraryId,
        IAuthorizedEndpointHandler endpointHandler,
        [FromServices] IPlatformUserReadService service) =>
        await endpointHandler.WithUser(user =>
            service.GetLibraryInformation(user.Id, libraryId));

    private static async Task<IResult> EditLibrary(
        [FromBody] EditLibraryRequest request,
        IAuthorizedEndpointHandler endpointHandler,
        [FromServices] IPlatformUserWriteService service) =>
        await endpointHandler.WithUser(user =>
        {
            var (Image, IsUpdated) = request.Avatar.ImageStringToBytes();
            return service.EditLibraryInfo(
                user.Id,
                request.LibraryId,
                request.Bio,
                Image,
                IsUpdated,
                request.LinksToSocialMedia);
        });

    private static async Task<IResult> SubscribeToLibrary(
        [FromBody] LibrarySubscriptionRequest request,
        IAuthorizedEndpointHandler endpointHandler,
        [FromServices] IPlatformUserWriteService service) =>
        await endpointHandler.WithUser(user =>
            service.SubscribeToLibrary(user.Id, request.LibraryId));

    private static async Task<IResult> UnsubscribeFromLibrary(
        [FromBody] LibrarySubscriptionRequest request,
        IAuthorizedEndpointHandler endpointHandler,
        [FromServices] IPlatformUserWriteService service) =>
        await endpointHandler.WithUser(user =>
            service.UnsubscribeFromLibrary(user.Id, request.LibraryId));

}
