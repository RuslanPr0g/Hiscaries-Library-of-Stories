namespace HC.API.Requests.Stories;

public class PublishStoryRequest
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string AuthorName { get; set; }

    public IEnumerable<Guid> GenreIds { get; set; }

    public int AgeLimit { get; set; }

    public string ImagePreview { get; set; }

    public DateTime DateWritten { get; set; }
}