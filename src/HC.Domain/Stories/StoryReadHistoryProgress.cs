using System;

namespace HC.Domain.Stories;

public class StoryReadHistoryProgress
{
    public int StoryId { get; init; }
    public int PublisherId { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string AuthorName { get; init; }
    public int AgeLimit { get; init; }
    public byte[] ImagePreview { get; init; }
    public DateTime DatePublished { get; init; }
    public DateTime DateWritten { get; init; }
    public DateTime LastRead { get; init; }
    public int Total { get; init; }
    public int Count { get; init; }
    public double Percentage { get; private set; }

    public void UpdatePercentage(double percentage)
    {
        Percentage = percentage;
    }
}