namespace HC.Application.DTOs;

public class PublishReviewDto
{
    public int Id { get; set; }
    public string PublisherId { get; set; }
    public string ReviewerId { get; set; }
    public string Message { get; set; }
}