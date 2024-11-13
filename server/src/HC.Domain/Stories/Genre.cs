namespace HC.Domain.Stories;

// TODO: Maybe genre is a separate aggregate root?
public sealed class Genre : Entity<GenreId>
{
    private Genre(
        GenreId id,
        string name,
        string description,
        byte[] imagePreview) : base(id)
    {
        Name = name;
        Description = description;
        ImagePreview = imagePreview;
    }

    private Genre(GenreId id) : base(id)
    {
    }

    public static Genre Create(
        GenreId id,
        string name,
        string description,
        byte[] imagePreview) =>
        new(id, name, description, imagePreview);

    public static Genre Create(GenreId id) => new(id);

    public string Name { get; private set; }
    public string Description { get; private set; }
    public byte[] ImagePreview { get; private set; }

    public void UpdateInformation(string name, string description, byte[] imagePreview)
    {
        Name = name;
        Description = description;
        ImagePreview = imagePreview;
    }

    private Genre()
    {
    }
}