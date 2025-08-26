namespace Hiscary.Stories.Api.Rest.Requests.Comments;

public class UpdateCommentRequest
{
    public Guid CommentId { get; set; }
    public Guid StoryId { get; set; }
    public string Content { get; set; }
    public int Score { get; set; }
}