namespace Hiscary.Stories.Domain.Stories;

/// <summary>
/// Represents audio of a story, the actual data is on the disk,
/// this entity just stores reference to it in a way,
/// so that the id of the file is the same as the file name on the disk
/// </summary>
public sealed class StoryAudio : Entity<StoryAudioId>
{
    private StoryAudio(
        StoryAudioId id,
        string name) : base(id)
    {
        ArgumentNullException.ThrowIfNull(name);

        FileId = id.Value;
        Name = name;
    }

    public static StoryAudio Create(
        StoryAudioId id,
        string name) =>
        new(id, name);

    internal void UpdateInformation(Guid fileId, string name)
    {
        FileId = fileId;
        Name = name;
    }

    public Guid FileId { get; private set; }
    public string Name { get; private set; }

    private StoryAudio()
    {
    }
}