namespace HC.Stories.Api.Rest.Requests.Genres;

public sealed class CreateGenreRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public byte[] ImagePreview { get; set; }
}