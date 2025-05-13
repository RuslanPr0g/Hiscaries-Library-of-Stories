namespace HC.API.Requests.Stories;

public class ReadStoryRequest
{
    public Guid StoryId { get; set; }
    public int PageRead { get; set; }
}