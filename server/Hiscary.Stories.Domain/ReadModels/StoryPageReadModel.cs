﻿using Hiscary.Stories.Domain.Stories;

namespace Hiscary.Stories.Domain.ReadModels;

public sealed class StoryPageReadModel
{
    public Guid StoryId { get; set; }
    public int Page { get; init; }
    public string Content { get; init; }

    public static StoryPageReadModel FromDomainModel(StoryPage page)
    {
        return new StoryPageReadModel
        {
            StoryId = page.StoryId,
            Page = page.Page,
            Content = page.Content,
        };
    }
}