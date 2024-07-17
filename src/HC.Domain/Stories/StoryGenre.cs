

namespace HC.Domain.Stories;

public sealed record class StoryGenre
{
    public StoryGenre(int id, Story storyId, Genre genreId)
    {
        Id = id;
        StoryId = storyId;
        GenreId = genreId;
    }

    public int Id { get; init; }
    public Story StoryId { get; init; }
    public Genre GenreId { get; init; }

    private StoryGenre()
    {
    }
}