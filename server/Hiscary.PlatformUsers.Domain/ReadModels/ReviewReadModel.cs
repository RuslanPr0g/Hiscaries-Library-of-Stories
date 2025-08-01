﻿namespace Hiscary.PlatformUsers.Domain.ReadModels;

public sealed class ReviewReadModel
{
    public string Message { get; init; }

    public static ReviewReadModel FromDomainModel(Review review)
    {
        return new ReviewReadModel
        {
            Message = review.Message,
        };
    }
}