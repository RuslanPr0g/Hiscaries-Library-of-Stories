using HC.API.Extensions;
using HC.API.Requests;
using HC.Application.ResultModels.Response;
using HC.Application.Users.Command;
using HC.Application.Users.Command.LoginUser;
using HC.Application.Users.Command.PublishReview;
using HC.Application.Users.Command.RefreshToken;
using HC.Application.Users.Query;
using HC.Application.Users.Query.BecomePublisher;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace HC.API.Controllers;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/users")
            .WithTags("Users");

        group.MapGet("/", GetCurrentUser)
            .RequireAuthorization()
            .Produces<UserAccountOwnerReadModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapGet("/{username}", GetUserInfo)
            .RequireAuthorization()
            .Produces<UserAccountOwnerReadModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/register", RegisterUser)
            .AllowAnonymous()
            .Produces<UserAccountOwnerReadModel>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapPost("/login", LoginUser)
            .AllowAnonymous()
            .Produces<UserWithTokenResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithName("UserLogin");

        group.MapPost("/refresh-token", RefreshToken)
            .Produces<UserWithTokenResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/become-publisher", BecomePublisher)
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/reviews", PublishReview)
            .RequireAuthorization()
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapDelete("/reviews/{id}", DeleteReview)
            .RequireAuthorization()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPatch("/profile", UpdateUserData)
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }

    private static async Task<IResult> RegisterUser([FromBody] RegisterUserRequest request, [FromServices] IMediator mediator)
    {
        var command = new RegisterUserCommand
        {
            Username = request.Username,
            Email = request.Email,
            BirthDate = request.BirthDate,
            Password = request.Password
        };

        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> LoginUser([FromBody] UserLoginRequest request, [FromServices]  IMediator mediator)
    {
        var command = new LoginUserCommand
        {
            Username = request.Username,
            Password = request.Password
        };

        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> RefreshToken([FromBody] RefreshTokenRequest request, [FromServices] IMediator mediator)
    {
        var command = new RefreshTokenCommand
        {
            Token = request.Token,
            RefreshToken = request.RefreshToken
        };

        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> GetCurrentUser(HttpContext context, [FromServices] IMediator mediator)
    {
        var query = new GetUserInfoQuery { Username = context.User.GetUsername() };
        var result = await mediator.Send(query);
        return result.ToResult();
    }

    private static async Task<IResult> GetUserInfo(string username, [FromServices] IMediator mediator)
    {
        var query = new GetUserInfoQuery { Username = username };
        var result = await mediator.Send(query);
        return result.ToResult();
    }

    private static async Task<IResult> BecomePublisher(HttpContext context, [FromServices] IMediator mediator)
    {
        var command = new BecomePublisherCommand { Username = context.User.GetUsername() };
        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> PublishReview([FromBody] PublishReviewRequest request, [FromServices] IMediator mediator)
    {
        var command = new PublishReviewCommand
        {
            ReviewId = request.Id,
            PublisherId = request.PublisherId,
            ReviewerId = request.ReviewerId,
            Message = request.Message
        };
        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> DeleteReview(Guid id, [FromServices] IMediator mediator)
    {
        var command = new DeleteReviewCommand { ReviewId = id };
        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> UpdateUserData([FromBody] UpdateUserDataRequest request, HttpContext context, [FromServices] IMediator mediator)
    {
        var command = new UpdateUserDataCommand
        {
            Username = context.User.GetUsername(),
            Email = request.Email,
            BirthDate = request.BirthDate,
            PreviousPassword = request.PreviousPassword,
            NewPassword = request.NewPassword,
            UpdatedUsername = request.UpdatedUsername
        };
        var result = await mediator.Send(command);
        return result.ToResult();
    }
}
