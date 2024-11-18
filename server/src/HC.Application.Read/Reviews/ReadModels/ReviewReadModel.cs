using HC.Application.Read.Users.ReadModels;
using HC.Domain.PlatformUsers;

namespace HC.Application.Read.Reviews.ReadModels;

public sealed class ReviewReadModel
{
    public Guid Id { get; set; }
    public PlatformUserReadModel Publisher { get; init; }
    public PlatformUserReadModel Reviewer { get; init; }
    public string Message { get; init; }

    public static ReviewReadModel FromDomainModel(Review review, PlatformUserReadModel publisher, PlatformUserReadModel reviewer)
    {
        return new ReviewReadModel
        {
            Id = review.Id,
            Publisher = publisher,
            Reviewer = reviewer,
            Message = review.Message,
        };
    }
}