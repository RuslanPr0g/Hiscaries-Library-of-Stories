namespace HC.Stories.Api.Rest.Requests.Stories;

public sealed class GetStoryListRequest
{
    public Guid Id { get; set; }
    public string SearchTerm { get; set; }
    public string Genre { get; set; }
}