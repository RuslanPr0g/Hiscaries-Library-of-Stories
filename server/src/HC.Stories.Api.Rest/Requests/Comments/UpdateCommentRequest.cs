namespace HC.Stories.Api.Rest.Requests.Comments;

public class UpdateCommentRequest
{
    public Guid Id { get; set; }
    public Guid StoryId { get; set; }
    public string Content { get; set; }
}