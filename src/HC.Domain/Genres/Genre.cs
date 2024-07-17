using System;

namespace HC.Domain.Genres;

public class Genre : Entity<GenreId>
{
    private Genre(GenreId id, string name, string description, byte[] imagePreview) : base(id)
    {
        Name = name;
        Description = description;
        ImagePreview = imagePreview;
    }

    public static Genre Create(Guid id, string name, string description, byte[] imagePreview) =>
        new Genre(new GenreId(id), name, description, imagePreview);

    public string Name { get; init; }
    public string Description { get; init; }
    public byte[] ImagePreview { get; init; }

    protected Genre()
    {
    }
}