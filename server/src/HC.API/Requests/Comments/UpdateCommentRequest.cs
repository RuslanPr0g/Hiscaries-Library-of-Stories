using System;

namespace HC.API.Requests.Comments;

public class UpdateCommentRequest
{
    public Guid Id { get; set; }
    public Guid StoryId { get; set; }
    public string Content { get; set; }
}