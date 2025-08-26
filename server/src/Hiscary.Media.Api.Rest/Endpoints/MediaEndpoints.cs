using Hiscary.Shared.Domain.FileStorage;
using Microsoft.AspNetCore.Mvc;

namespace Hiscary.Media.Api.Rest.Endpoints;

public static class MediaEndpoints
{
    public static void MapMediaEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/media")
            .WithTags("Media");

        group.MapGet("/images/{fileName}", GetImage)
            .Produces<IResult>(StatusCodes.Status200OK, contentType: "application/octet-stream")
            .Produces(StatusCodes.Status401Unauthorized);
    }

    private static async Task<IResult> GetImage(
        [FromRoute] string fileName,
        [FromServices] IBlobStorageService service,
        CancellationToken cancellationToken)
    {
        try
        {
            var (content, contentType) = await service.DownloadAsync(
                "images",
                blobName: fileName,
                cancellationToken: cancellationToken);

            return Results.File(content, contentType);
        }
        catch (FileNotFoundException)
        {
            return Results.NotFound();
        }
    }
}
