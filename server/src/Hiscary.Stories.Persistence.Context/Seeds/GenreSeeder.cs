using Hiscary.Stories.Domain.Genres;

namespace Hiscary.Stories.Persistence.Context.Seeds;

public static class GenreSeeder
{
    public static void SeedAsync(StoriesContext context)
    {
        if (context.Genres.Any()) return;

        var genres = new List<Genre>
        {
            Genre.Create(Guid.NewGuid(), "Fantasy", "Genre with magical or supernatural elements, often set in fictional worlds.", []),
            Genre.Create(Guid.NewGuid(), "Science Fiction", "Explores futuristic technology, space exploration, and scientific concepts.", []),
            Genre.Create(Guid.NewGuid(), "Mystery", "Focused on solving a crime, puzzle, or uncovering secrets.", []),
            Genre.Create(Guid.NewGuid(), "Thriller", "Designed to elicit excitement and suspense, often with high stakes.", []),
            Genre.Create(Guid.NewGuid(), "Romance", "Centers on love and relationships between characters.", []),
            Genre.Create(Guid.NewGuid(), "Historical Fiction", "Set in the past, often with real historical figures or events.", []),
            Genre.Create(Guid.NewGuid(), "Horror", "Intended to frighten, scare, or disgust the reader.", []),
            Genre.Create(Guid.NewGuid(), "Adventure", "Exciting stories with exploration, danger, or risk.", []),
            Genre.Create(Guid.NewGuid(), "Drama", "Character-driven stories with emotional themes.", []),
            Genre.Create(Guid.NewGuid(), "Comedy", "Light-hearted and humorous, often with satire.", []),
            Genre.Create(Guid.NewGuid(), "Biography", "Narrative of a person's life written by someone else.", []),
            Genre.Create(Guid.NewGuid(), "Autobiography", "Narrative of a person's life written by themselves.", []),
            Genre.Create(Guid.NewGuid(), "Non-Fiction", "Based on facts and real events.", []),
            Genre.Create(Guid.NewGuid(), "Poetry", "Literary work focused on expression through rhythm and style.", []),
            Genre.Create(Guid.NewGuid(), "Satire", "Uses humor, irony, or ridicule to expose and criticize.", [])
        };

        context.Genres.AddRange(genres);
        context.SaveChanges();
    }
}
