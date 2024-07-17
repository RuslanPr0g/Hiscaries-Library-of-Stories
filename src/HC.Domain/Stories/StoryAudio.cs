using System;

namespace HC.Domain.Stories;

/// <summary>
/// Represents audio of a story, the actual data is on the disk,
/// this entity just stores reference to it in a way,
/// so that the id of the file is the same as the file name on the disk
/// </summary>
public sealed class StoryAudio : Entity<StoryAudioId>
{
    private StoryAudio(
        StoryAudioId id,
        DateTime dateAdded,
        string name) : base(id)
    {
        ArgumentNullException.ThrowIfNull(name);

        FileId = id.Value;
        DateAdded = dateAdded;
        Name = name;
    }

    public static StoryAudio Create(
        Guid id,
        DateTime dateAdded,
        string name) =>
        new StoryAudio(new StoryAudioId(id), dateAdded, name);

    public Guid FileId { get; init; }
    public DateTime DateAdded { get; init; }
    public string Name { get; init; }

    protected StoryAudio()
    {
    }
}