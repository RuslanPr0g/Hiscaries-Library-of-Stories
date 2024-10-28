using HC.Domain.Users;

namespace HC.Application.Read.Stories.ReadModels;

public sealed class StoryWithProgressReadModel
{
    public StorySimpleReadModel Story { get; set; }
    public decimal ReadPercentage { get; set; }

    public static StoryWithProgressReadModel FromDomainModel(UserReadHistory history)
    {
        return new StoryWithProgressReadModel
        {
            Story = StorySimpleReadModel.FromDomainModel(history.Story),
            ReadPercentage = history.LastPageRead / history.Story.Contents.Count * 100
        };
    }
}