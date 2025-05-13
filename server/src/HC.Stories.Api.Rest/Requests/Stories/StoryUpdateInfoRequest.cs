namespace HC.API.Requests.Stories;

public class StoryUpdateInfoRequest
{
    public Guid? StoryId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string AuthorName { get; set; }

    public IEnumerable<Guid> GenreIds { get; set; }

    public int AgeLimit { get; set; }

    public string ImagePreview { get; set; }

    public DateTime DateWritten { get; set; }

    public IEnumerable<string> Contents { get; set; }
}