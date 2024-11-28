using HC.API.Extensions;
using HC.API.Requests.Users;
using HC.Application.Read.Users.ReadModels;
using HC.Application.Write.ResultModels.Response;
using HC.Application.Write.UserAccounts.Command.CreateUser;
using HC.Application.Write.UserAccounts.Command.LoginUser;
using HC.Application.Write.UserAccounts.Command.RefreshToken;
using HC.Application.Write.UserAccounts.Command.UpdateUserData;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace HC.API.ApiEndpoints;

public static class UserAccountEndpoints
{
    public static void MapUserAccountEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/accounts")
            .WithTags("Accounts");

        group.MapPost("/register", RegisterUser)
            .AllowAnonymous()
            .Produces<PlatformUserReadModel>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapPost("/login", LoginUser)
            .AllowAnonymous()
            .Produces<UserWithTokenResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/refresh-token", RefreshToken)
            .Produces<UserWithTokenResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPatch("/", UpdateUserData)
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

        return await mediator.SendMessageGetResult(command);
    }

    private static async Task<IResult> LoginUser([FromBody] UserLoginRequest request, [FromServices] IMediator mediator)
    {
        var command = new LoginUserCommand
        {
            Username = request.Username,
            Password = request.Password
        };

        return await mediator.SendMessageGetResult(command);
    }

    private static async Task<IResult> RefreshToken([FromBody] RefreshTokenRequest request, [FromServices] IMediator mediator)
    {
        var command = new RefreshTokenCommand
        {
            Token = request.Token,
            RefreshToken = request.RefreshToken
        };

        return await mediator.SendMessageGetResult(command);
    }

    private static async Task<IResult> UpdateUserData([FromBody] UpdateUserDataRequest request, HttpContext context, [FromServices] IMediator mediator)
    {
        var userIdClaim = context.User.GetUserId();
        if (userIdClaim is null)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var command = new UpdateUserDataCommand
        {
            Id = userIdClaim.Value,
            Username = request.UpdatedUsername,
            Email = request.Email,
            BirthDate = request.BirthDate,
            PreviousPassword = request.PreviousPassword,
            NewPassword = request.NewPassword,
        };

        return await mediator.SendMessageGetResult(command);
    }
}
