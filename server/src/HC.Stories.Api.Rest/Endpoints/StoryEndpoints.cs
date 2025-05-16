using Enterprise.Api.Rest;
using Enterprise.Domain.Extensions;
using Enterprise.Domain.ResultModels.Response;
using HC.Stories.Api.Rest.Requests.Comments;
using HC.Stories.Api.Rest.Requests.Stories;
using HC.Stories.Domain.ReadModels;
using Microsoft.AspNetCore.Mvc;

namespace HC.Stories.Api.Rest.Endpoints;

public static class StoryEndpoints
{
    public static void MapStoriesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/stories")
            .WithTags("Stories")
            .RequireAuthorization();

        group.MapPost("/healthcheck", () =>
        {
            return Results.Ok("STORIES SERVICE WORKS!");
        })
            .AllowAnonymous()
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("/search", GetStories)
            .Produces<IEnumerable<StoryWithContentsReadModel>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/by-id-with-contents", GetByIdWithContents)
            .Produces<IEnumerable<StoryWithContentsReadModel>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapGet("/genres", GetGenres)
            .Produces<IEnumerable<GenreReadModel>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapGet("/recommendations", BestToRead)
            .Produces<IEnumerable<StorySimpleReadModel>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapGet("/resume-reading", ResumeReading)
            .Produces<IEnumerable<StorySimpleReadModel>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapGet("/reading-history", ReadingHistory)
            .Produces<IEnumerable<StorySimpleReadModel>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapGet("/", GetStoriesBy)
            .Produces<IEnumerable<StorySimpleReadModel>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/", PublishStory)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPatch("/", UpdateStoryInformation)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/comments", AddComment)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/score", ScoreStory)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapDelete("/comments", DeleteComment)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapDelete("/", DeleteStory)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPatch("/comments", UpdateComment)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapDelete("/audio", DeleteAudioForStory)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }

    private static async Task<IResult> GetStoriesBy(
        [FromQuery] Guid libraryId,
        [FromServices] IStoryReadService service,
        HttpContext context)
    {
        var requesterUsername = context.User.GetUsername();
        var userIdClaim = context.User.GetUserId();
        if (!userIdClaim.HasValue || requesterUsername is null)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var result = await service.SearchForStory(
            userId: userIdClaim.Value,
            libraryId: libraryId);

        return result.ToResult();
    }

    private static async Task<IResult> GetStories(
        [FromBody] GetStoryListRequest request,
        [FromServices] IStoryReadService service,
        HttpContext context)
    {
        var requesterUsername = context.User.GetUsername();
        var userIdClaim = context.User.GetUserId();
        if (!userIdClaim.HasValue || requesterUsername is null)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var result = await service.SearchForStory(
            userId: userIdClaim.Value,
            searchTerm: request.SearchTerm,
            genre: request.Genre,
            requesterUsername: requesterUsername);

        return result.ToResult();
    }

    private static async Task<IResult> GetByIdWithContents(
        [FromBody] GetStoryWithContentsRequest request,
        [FromServices] IStoryReadService service,
        HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.GetUserId();
        if (userIdClaim is null)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var result = await service.GetStoryById(request.Id, userIdClaim.Value);

        return result.ToResult();
    }

    private static async Task<IResult> GetGenres([FromServices] IStoryReadService service)
    {
        var result = await service.GetAllGenres();
        return result.ToResult();
    }

    private static async Task<IResult> BestToRead(
        [FromServices] IStoryReadService service,
        HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.GetUserId();
        if (userIdClaim is null)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var result = await service.GetStoryRecommendations(userIdClaim.Value);
        var response = result.ToList();
        response.Shuffle();

        return response.ToResult();
    }

    private static async Task<IResult> ResumeReading(
        [FromServices] IStoryReadService service,
        HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.GetUserId();
        if (userIdClaim is null)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var result = await service.GetStoryResumeReading(userIdClaim.Value);

        return result.ToResult();
    }

    private static async Task<IResult> ReadingHistory(
        [FromServices] IStoryReadService service,
        HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.GetUserId();
        if (userIdClaim is null)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var result = await service.GetStoryReadingHistory(userIdClaim.Value);

        return result.ToResult();
    }

    private static async Task<IResult> PublishStory(
        [FromBody] PublishStoryRequest request,
        [FromServices] IStoryWriteService service,
        HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.GetUserId();
        if (userIdClaim is null)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var (Image, IsUpdated) = request.ImagePreview.ImageStringToBytes();

        var result = await service.PublishStory(
            userIdClaim.Value,
            request.LibraryId,
            request.Title,
            request.Description,
            request.AuthorName,
            request.GenreIds,
            request.AgeLimit,
            Image,
            IsUpdated,
            request.DateWritten);

        return result.ToResult();
    }

    private static async Task<IResult> UpdateStoryInformation(
        [FromBody] StoryUpdateInfoRequest request,
        [FromServices] IStoryWriteService service,
        HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.GetUserId();
        if (userIdClaim is null)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var (Image, IsUpdated) = request.ImagePreview.ImageStringToBytes();

        var result = await service.UpdateStory(
            userIdClaim.Value,
            request.StoryId,
            request.Title,
            request.Description,
            request.AuthorName,
            request.GenreIds,
            request.AgeLimit,
            Image,
            IsUpdated,
            request.DateWritten,
            request.Contents);

        return result.ToResult();
    }

    private static async Task<IResult> AddComment(
        [FromBody] CreateCommentRequest request,
        [FromServices] IStoryWriteService service,
        HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.GetUserId();
        if (userIdClaim is null)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var result = await service.AddComment(
            request.StoryId,
            userIdClaim.Value,
            request.Content,
            request.Score);

        return result.ToResult();
    }

    private static async Task<IResult> ScoreStory(
        [FromBody] ScoreStoryRequest request,
        [FromServices] IStoryWriteService service,
        HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.GetUserId();
        if (userIdClaim is null)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var result = await service.SetStoryScoreForAUser(
            request.StoryId,
            userIdClaim.Value,
            request.Score);

        return result.ToResult();
    }

    private static async Task<IResult> DeleteComment(
        Guid storyId,
        Guid commentId,
        [FromServices] IStoryWriteService service)
    {
        var result = await service.DeleteComment(
            storyId,
            commentId);

        return result.ToResult();
    }

    private static async Task<IResult> DeleteStory(
        Guid storyId,
        [FromServices] IStoryWriteService service)
    {
        var result = await service.DeleteStory(storyId);

        return result.ToResult();
    }

    private static async Task<IResult> UpdateComment(
        [FromBody] UpdateCommentRequest request,
        [FromServices] IStoryWriteService service)
    {
        var result = await service.UpdateComment(
            request.CommentId,
            request.StoryId,
            request.Content,
            request.Score);

        return result.ToResult();
    }

    private static async Task<IResult> DeleteAudioForStory(
        Guid storyId,
        [FromServices] IStoryWriteService service)
    {
        var result = await service.DeleteAudio(storyId);

        return result.ToResult();
    }
}
