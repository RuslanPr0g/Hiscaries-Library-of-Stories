using Enterprise.Api.Rest;
using Enterprise.Domain.ResultModels.Response;
using HC.UserAccounts.Api.Rest.Requests;
using HC.UserAccounts.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace HC.UserAccounts.Api.Rest.Endpoints;

public static class UserAccountEndpoints
{
    public static void MapUserAccountsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/accounts")
            .WithTags("Accounts");

        group.MapPost("/register", RegisterUser)
            .AllowAnonymous()
            .Produces<TokenMetadata>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapPost("/login", LoginUser)
            .AllowAnonymous()
            .Produces<TokenMetadata>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/refresh-token", RefreshToken)
            .Produces<TokenMetadata>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPatch("/", UpdateUserData)
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }

    private static async Task<IResult> RegisterUser(
        [FromBody] RegisterUserRequest request,
        [FromServices] IUserAccountWriteService service)
    {
        var result = await service.RegisterUser(
            request.Username,
            request.Email,
            request.Password,
            request.BirthDate);

        return result.ToResult();
    }

    private static async Task<IResult> LoginUser(
        [FromBody] UserLoginRequest request,
        [FromServices] IUserAccountWriteService service)
    {
        var result = await service.LoginUser(
            request.Username,
            request.Password);

        return result.ToResult();
    }

    private static async Task<IResult> RefreshToken(
        [FromBody] RefreshTokenRequest request,
        HttpContext context,
        [FromServices] IUserAccountWriteService service)
    {
        var username = context.User.GetUsername();
        if (username is null)
        {
            return Results.BadRequest("Invalid or missing username claim in the token.");
        }

        var result = await service.RefreshToken(
            username,
            request.Token,
            request.RefreshToken);

        return result.ToResult();
    }

    private static async Task<IResult> UpdateUserData(
        [FromBody] UpdateUserDataRequest request,
        HttpContext context,
        [FromServices] IUserAccountWriteService service)
    {
        var userId = context.User.GetUserId();
        if (!userId.HasValue)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var result = await service.UpdateUserData(
            userId.Value,
            request.UpdatedUsername,
            request.Email,
            request.BirthDate,
            request.PreviousPassword,
            request.NewPassword);

        return result.ToResult();
    }
}
