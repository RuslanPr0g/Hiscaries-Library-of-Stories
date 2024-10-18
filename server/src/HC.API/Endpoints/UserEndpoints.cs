﻿using HC.API.Extensions;
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
            .Produces<UserReadModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapGet("/{username}", GetUserInfo)
            .RequireAuthorization()
            .Produces<UserReadModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/register", RegisterUser)
            .AllowAnonymous()
            .Produces<UserReadModel>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapPost("/login", LoginUser)
            .AllowAnonymous()
            .Produces<UserWithTokenResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

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

    private static async Task<IResult> RegisterUser(RegisterUserRequest request, IMediator mediator)
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

    private static async Task<IResult> LoginUser(UserLoginRequest request, IMediator mediator)
    {
        var command = new LoginUserCommand
        {
            Username = request.Username,
            Password = request.Password
        };

        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> RefreshToken(RefreshTokenRequest request, IMediator mediator)
    {
        var command = new RefreshTokenCommand
        {
            Token = request.Token,
            RefreshToken = request.RefreshToken
        };

        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> GetCurrentUser(HttpContext context, IMediator mediator)
    {
        var query = new GetUserInfoQuery { Username = context.User.GetUsername() };
        var result = await mediator.Send(query);
        return result.ToResult();
    }

    private static async Task<IResult> GetUserInfo(string username, IMediator mediator)
    {
        var query = new GetUserInfoQuery { Username = username };
        var result = await mediator.Send(query);
        return result.ToResult();
    }

    private static async Task<IResult> BecomePublisher(HttpContext context, IMediator mediator)
    {
        var command = new BecomePublisherCommand { Username = context.User.GetUsername() };
        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> PublishReview(PublishReviewRequest request, IMediator mediator)
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

    private static async Task<IResult> DeleteReview(Guid id, IMediator mediator)
    {
        var command = new DeleteReviewCommand { ReviewId = id };
        var result = await mediator.Send(command);
        return result.ToResult();
    }

    private static async Task<IResult> UpdateUserData(UpdateUserDataRequest request, HttpContext context, IMediator mediator)
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