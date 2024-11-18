using Bogus;
using HC.Application.Write.Stories.Command;
using HC.Application.Write.Stories.Services;
using HC.Domain.Stories;
using HC.Persistence.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HC.API.ApiEndpoints;

public class GenerateStoryRequest
{
    public int Count { get; set; }
}

public static class ContentGenerationEndpoints
{
    public static void MapContentGenerationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/content-generation")
            .WithTags("ContentGeneration");

        group.MapPost("/stories", GenerateStories)
            .Produces<int>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);
    }

    private static async Task<IResult> GenerateStories(
        [FromBody] GenerateStoryRequest request,
        [FromServices] HiscaryContext hiscaryContext,
        [FromServices] IStoryWriteService storyService)
    {
        return Results.Ok(await GenerateNStories(request.Count, hiscaryContext, storyService));
    }

    private static async Task<List<Guid>> GenerateNStories(
    int storiesCount,
    HiscaryContext hiscaryContext,
    IStoryWriteService storyService)
    {
        var generatedStories = new List<Guid>(storiesCount);

        var publisher = await hiscaryContext.PlatformUsers.FirstOrDefaultAsync();
        var genre = await hiscaryContext.Genres.FirstOrDefaultAsync();

        if (publisher is null || genre is null)
        {
            throw new Exception("No data is present in the database to seed upon");
        }

        for (int i = 0; i < storiesCount; i++)
        {
            var imagePreview = await GetRandomImageAsByteArray();

            var command = GenerateFakeStoryCommand(
                publisher.Id,
                [genre.Id],
                imagePreview);

            var publishedStory = await storyService.PublishStory(command);
            await hiscaryContext.SaveChangesAsync();

            var generatedStory = await hiscaryContext.Stories.FirstOrDefaultAsync(s => s.Id == new StoryId(publishedStory.Value.Id));

            IEnumerable<string> contents = GenerateParagraphs(new Random(storiesCount).Next(7, 50));

            generatedStory.ModifyContents(contents);

            generatedStories.Add(publishedStory.Value.Id);
            await hiscaryContext.SaveChangesAsync();
        }

        return generatedStories;
    }

    public static IEnumerable<string> GenerateParagraphs(int pagesCount)
    {
        var faker = new Faker();

        for (int i = 0; i < pagesCount; i++)
        {
            yield return GetParagraph(faker, pagesCount);
        }

        static string GetParagraph(Faker faker,int seed = 435353)
        {
            var text = faker.Lorem.Paragraph(new Random(seed).Next(90, 270));
            return $@"<div><br/>{text}<br/></div>";
        }
    }

    private static PublishStoryCommand GenerateFakeStoryCommand(
        Guid publisherId,
        IEnumerable<Guid> genreIds,
        byte[] imagePreview
        )
    {
        var faker = new Faker<PublishStoryCommand>()
            .RuleFor(s => s.LibraryId, f => publisherId)
            .RuleFor(s => s.Title, f => f.Lorem.Sentence(5, 10))
            .RuleFor(s => s.Description, f => f.Lorem.Paragraphs(4, 8))
            .RuleFor(s => s.AuthorName, f => f.Name.FullName())
            .RuleFor(s => s.AgeLimit, f => f.Random.Int(0, 18))
            .RuleFor(s => s.GenreIds, f => genreIds)
            .RuleFor(s => s.ImagePreview, f => imagePreview)
            .RuleFor(s => s.ShouldUpdateImage, f => true)
            .RuleFor(s => s.DateWritten, f => DateTime.UtcNow);

        return faker.Generate();
    }

    private static async Task<byte[]> GetRandomImageAsByteArray()
    {
        using var httpClient = new HttpClient();
        try
        {
            var response = await httpClient.GetAsync("https://picsum.photos/1920/1080");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching image: {ex.Message}");
            return [];
        }
    }
}
