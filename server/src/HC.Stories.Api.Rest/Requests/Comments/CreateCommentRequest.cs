using System;

namespace HC.API.Requests.Comments;

public class CreateCommentRequest
{
    public Guid Id { get; set; }
    public Guid StoryId { get; set; }
    public string Content { get; set; }
}