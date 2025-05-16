using Enterprise.Api.Rest;
using Enterprise.Domain.ResultModels.Response;
using HC.Notifications.Api.Rest.Requests;
using HC.Notifications.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace HC.Notifications.Api.Rest.Endpoints;

public static class NotificationsEndpoints
{
    public static void MapNotificationsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/notifications")
            .WithTags("Notifications");

        group.MapPost("/healthcheck", () =>
        {
            return Results.Ok("NOTIFICATIONS SERVICE WORKS!");
        })
            .AllowAnonymous()
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("/", GetNotifications)
            .Produces<TokenMetadata>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/", ReadNotifications)
            .Produces<TokenMetadata>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);
    }

    private static async Task<IResult> GetNotifications(
        HttpContext context,
        [FromServices] INotificationReadService service)
    {
        var userIdClaim = context.User.GetUserId();
        if (!userIdClaim.HasValue)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var result = await service.GetNotifications(userIdClaim.Value);

        return result.ToResult();
    }

    private static async Task<IResult> ReadNotifications(
        [FromBody] ReadNotificationsRequest request,
        HttpContext context,
        [FromServices] INotificationWriteService service)
    {
        var userIdClaim = context.User.GetUserId();
        if (!userIdClaim.HasValue)
        {
            return Results.BadRequest("Invalid or missing ID claim in the token.");
        }

        var result = await service.ReadNotifications(userIdClaim.Value, request.NotificationIds);

        return result.ToResult();
    }
}
