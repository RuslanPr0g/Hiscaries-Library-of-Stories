using HC.Domain.Stories;
using System.Collections.Generic;
using System.Linq;

public sealed class StoryWithContentsReadModel : StorySimpleReadModel
{
    public byte[] Audio { get; set; }
    public IEnumerable<GenreReadModel> Genres { get; set; }
    public IEnumerable<StoryPageReadModel> Pages { get; set; }

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
            Publisher = UserAccountOwnerReadModel.FromDomainModel(story.Publisher),
            Genres = story.Genres.Select(GenreReadModel.FromDomainModel),
            Pages = story.Contents.Select(StoryPageReadModel.FromDomainModel),
        };
    }
}