using Enterprise.Api.Rest;
using Enterprise.Domain.ResultModels.Response;
using Hiscary.UserAccounts.Api.Rest.Requests;
using Hiscary.UserAccounts.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hiscary.UserAccounts.Api.Rest.Endpoints;

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

        return result.OperationToHttpResult();
    }

    private static async Task<IResult> LoginUser(
        [FromBody] UserLoginRequest request,
        [FromServices] IUserAccountWriteService service)
    {
        var result = await service.LoginUser(
            request.Username,
            request.Password);

        return result.OperationToHttpResult();
    }

    private static async Task<IResult> RefreshToken(
        [FromBody] RefreshTokenRequest request,
        IAuthorizedEndpointHandler endpointHandler,
        [FromServices] IUserAccountWriteService service) =>
        await endpointHandler.WithUser(user =>
            service.RefreshToken(
                user.Username,
                request.Token,
                request.RefreshToken));

    private static async Task<IResult> UpdateUserData(
        [FromBody] UpdateUserDataRequest request,
        IAuthorizedEndpointHandler endpointHandler,
        [FromServices] IUserAccountWriteService service) =>
        await endpointHandler.WithUser(user =>
            service.UpdateUserData(
                user.Id,
                request.UpdatedUsername,
                request.Email,
                request.BirthDate,
                request.PreviousPassword,
                request.NewPassword));

}
