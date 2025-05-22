namespace HC.PlatformUsers.Domain.ReadModels;

public sealed class UserReadingStoryMetadataReadModel
{
    public Guid StoryId { get; set; }
    public string? LibraryName { get; set; }
    public bool IsEditable { get; set; } = false;
    public decimal PercentageRead { get; set; } = 0;
    public int LastPageRead { get; set; } = 0;
}
