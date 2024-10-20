using HC.API.Extensions;
using HC.API.Request;
using HC.API.Requests;
using HC.Application.Common.Extentions;
using HC.Application.Stories.Command;
using HC.Application.Stories.Command.DeleteStory;
using HC.Application.Stories.Command.ReadStory;
using HC.Application.Stories.Command.ScoreStory;
using HC.Application.Stories.Query;
using HC.Application.Stories.Query.GetGenreList;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HC.API.Controllers;

public static class StoryEndpoints
{
    public static void MapStoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/stories")
            .WithTags("Stories")
            .RequireAuthorization();

        group.MapPost("/", GetStories)
            .Produces<IEnumerable<StoryReadModel>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/genres", AddGenre)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPatch("/genres", EditGenre)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapDelete("/genres", DeleteGenre)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapGet("/genres", GetGenres)
            .Produces<IEnumerable<GenreReadModel>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapGet("/shuffle", BestToRead)
            .Produces<IEnumerable<StoryReadModel>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/publish", PublishStory)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPatch("/", UpdateStoryInformation)
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

        group.MapPost("/audio", AddAudioForStory)
            .AllowAnonymous()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapPut("/audio", ChangeAudioForStory)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapDelete("/audio", DeleteAudioForStory)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }

    private static async Task<IResult> GetStories([FromBody] GetStoryListRequest request, [FromServices]  IMediator mediator)
    {
        var query = new GetStoryListQuery
        {
            Id = request.Id,
            SearchTerm = request.Search,
            Genre = request.Genre
        };

        var result = await mediator.Send(query);
        return Results.Ok(result);
    }

    private static async Task<IResult> AddGenre([FromBody] CreateGenreRequest request, [FromServices] IMediator mediator)
    {
        var command = new CreateGenreCommand
        {
            Name = request.Name,
            Description = request.Description,
            ImagePreview = request.ImagePreview
        };

        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> EditGenre([FromBody] UpdateGenreRequest request, [FromServices] IMediator mediator)
    {
        var command = new UpdateGenreCommand
        {
            GenreId = request.Id,
            Name = request.Name,
            Description = request.Description,
            ImagePreview = request.ImagePreview
        };

        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> DeleteGenre([FromBody] DeleteGenreRequest request, [FromServices] IMediator mediator)
    {
        var command = new DeleteGenreCommand
        {
            GenreId = request.Id,
        };

        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> GetGenres([FromServices] IMediator mediator)
    {
        var query = new GetGenreListQuery();
        var result = await mediator.Send(query);
        return result.ToResult();
    }

    private static async Task<IResult> BestToRead([FromServices] IMediator mediator)
    {
        var query = new GetStoryRecommendationsQuery
        {
            Username = string.Empty
        };

        var result = await mediator.Send(query);
        var response = result.ToList();
        response.Shuffle();

        return Results.Ok(response);
    }

    private static async Task<IResult> PublishStory([FromBody] PublishStoryRequest request, [FromServices] IMediator mediator, HttpContext httpContext)
    {
        byte[] imageInBytes = null;
        if (request.ImagePreview != null)
        {
            int offset = request.ImagePreview.IndexOf(',') + 1;
            imageInBytes = Convert.FromBase64String(request.ImagePreview[offset..]);
        }

        var publisherIdClaim = httpContext.User.FindFirst("id");
        if (publisherIdClaim is null || !Guid.TryParse(publisherIdClaim.Value, out Guid publisherId))
        {
            return Results.BadRequest("Invalid or missing publisher ID in the token.");
        }

        var command = new CreateStoryCommand
        {
            PublisherId = publisherId,
            Title = request.Title,
            Description = request.Description,
            AuthorName = request.AuthorName,
            GenreIds = request.GenreIds,
            AgeLimit = request.AgeLimit,
            DateWritten = request.DateWritten,
            ImagePreview = imageInBytes
        };

        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> DeleteAudioForStory(Guid storyId, [FromServices] IMediator mediator)
    {
        var command = new DeleteStoryAudioCommand
        {
            StoryId = storyId
        };

        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> UpdateStoryInformation([FromBody] StoryUpdateInfoRequest request, [FromServices] IMediator mediator)
    {
        byte[] imageInBytes = null;
        if (request.ImagePreview != null)
        {
            int offset = request.ImagePreview.IndexOf(',') + 1;
            imageInBytes = Convert.FromBase64String(request.ImagePreview[offset..]);
        }

        // TODO: Add ability to set pages content
        var command = new UpdateStoryCommand
        {
            Title = request.Title,
            Description = request.Description,
            AuthorName = request.AuthorName,
            GenreIds = request.GenreIds,
            AgeLimit = request.AgeLimit,
            ImagePreview = imageInBytes,
            StoryId = request.StoryId.Value
        };

        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> ReadStory([FromBody] ReadStoryRequest request, [FromServices] IMediator mediator)
    {
        var command = new ReadStoryCommand
        {
            UserId = new Guid(),
            StoryId = new Guid(),
            Page = request.PageRead
        };

        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> BookmarkStory([FromBody] BookmarkStoryRequest request, [FromServices] IMediator mediator)
    {
        var command = new BookmarkStoryCommand
        {
            UserId = new Guid(),
            StoryId = new Guid()
        };

        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> AddComment([FromBody] CreateCommentRequest request, [FromServices] IMediator mediator)
    {
        var command = new AddCommentCommand
        {
            StoryId = request.StoryId,
            UserId = new Guid(),
            Content = request.Content
        };

        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> ScoreStory([FromBody] ScoreStoryRequest request, [FromServices] IMediator mediator)
    {
        var command = new StoryScoreCommand
        {
            StoryId = request.StoryId,
            Score = request.Score,
            UserId = new Guid()
        };

        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> DeleteComment(Guid commentId, [FromServices] IMediator mediator)
    {
        var command = new DeleteCommentCommand
        {
            StoryId = commentId
        };

        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> DeleteStory(Guid storyId, [FromServices] IMediator mediator)
    {
        var command = new DeleteStoryCommand
        {
            StoryId = storyId
        };

        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> UpdateComment([FromBody] UpdateCommentRequest request, [FromServices] IMediator mediator)
    {
        var command = new UpdateCommentCommand
        {
            CommentId = request.Id,
            StoryId = request.StoryId,
            Content = request.Content
        };

        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> AddAudioForStory([FromBody] UpdateAudioRequest request, [FromServices] IMediator mediator)
    {
        var command = new UpdateStoryAudioCommand
        {
            StoryId = request.StoryId,
            Name = request.Name,
            Audio = request.Audio
        };

        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> ChangeAudioForStory([FromBody] UpdateAudioRequest request, [FromServices] IMediator mediator)
    {
        var command = new UpdateStoryAudioCommand
        {
            StoryId = request.StoryId,
            Name = request.Name,
            Audio = request.Audio
        };

        var result = await mediator.Send(command);
        return result.ToResult();
    }
}
