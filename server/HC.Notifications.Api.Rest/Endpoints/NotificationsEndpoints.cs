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

        group.MapGet("/", GetNotifications)
            .Produces<TokenMetadata>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/", ReadNotifications)
            .Produces<TokenMetadata>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);
    }

    private static async Task<IResult> GetNotifications(
        IAuthorizedEndpointHandler endpointHandler,
        [FromServices] INotificationReadService service) =>
            await endpointHandler.WithUser((CurrentUser user) =>
                service.GetNotifications(user.Id));

    private static async Task<IResult> ReadNotifications(
        [FromBody] ReadNotificationsRequest request,
        IAuthorizedEndpointHandler endpointHandler,
        [FromServices] INotificationWriteService service) =>
            await endpointHandler.WithUser((CurrentUser user) =>
                service.ReadNotifications(user.Id, request.NotificationIds));
}
