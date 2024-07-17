namespace HC.Application.DTOs;

public class CreateCommentDto
{
    public int Id { get; set; }
    public string StoryId { get; set; }
    public string Content { get; set; }
}