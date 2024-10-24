﻿using HC.Domain.Stories;
using System.Collections.Generic;
using System.Linq;

public sealed class StoryWithContentsReadModel : StorySimpleReadModel
{
    public byte[] Audio { get; set; }
    public IEnumerable<GenreReadModel> Genres { get; set; }
    public IEnumerable<StoryPageReadModel> Contents { get; set; }

    public static StoryWithContentsReadModel FromDomainModel(Story story)
    {
        return new StoryWithContentsReadModel
        {
            Id = story.Id,
            Title = story.Title,
            Description = story.Description,
            AuthorName = story.AuthorName,
            AgeLimit = story.AgeLimit,
            ImagePreview = story.ImagePreview,
            DatePublished = story.DatePublished,
            DateWritten = story.DateWritten,
            Publisher = UserSimpleReadModel.FromDomainModel(story.Publisher),
            Genres = story.Genres?.Select(GenreReadModel.FromDomainModel) ?? Enumerable.Empty<GenreReadModel>(),
            Contents = story.Contents?.Select(StoryPageReadModel.FromDomainModel) ?? Enumerable.Empty<StoryPageReadModel>(),
        };
    }
}