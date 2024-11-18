using AutoFixture;
using FluentAssertions;
using HC.Domain.Genres;
using HC.Domain.Stories;

namespace HC.Domain.Tests.Stories;

public class StoryTests
{
    private readonly Fixture _fixture;

    public StoryTests()
    {
        _fixture = new Fixture();
    }

    private Story CreateTestStory()
    {
        return Story.Create(
            _fixture.Create<StoryId>(),
            _fixture.Create<LibraryId>(),
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            _fixture.Create<string>(),
            _fixture.Create<List<Genre>>(),
            _fixture.Create<int>(),
            _fixture.Create<DateTime>()
        );
    }

    [Fact]
    public void SetContents_AddsPages_WhenNewContentsAreLonger()
    {
        var editedAt = new DateTime(2002, 2, 3);

        // Arrange
        var obj = CreateTestStory();
        obj.ModifyContents(new List<string>
        {
            "Page 1"
        });

        var newContents = new List<string> { "Page 1", "Page 2" };

        // Act
        obj.ModifyContents(newContents);

        // Assert
        obj.Contents.Should().HaveCount(2);
        obj.Contents[0].Content.Should().Be("Page 1");
        obj.Contents[1].Content.Should().Be("Page 2");
    }

    [Fact]
    public void SetContents_RemovesPages_WhenNewContentsAreShorter()
    {
        var editedAt = new DateTime(2002, 2, 3);

        // Arrange
        var obj = CreateTestStory();
        obj.ModifyContents(new List<string>
        {
            "Page 1",
            "Page 2"
        });

        var newContents = new List<string> { "Page 1" };

        // Act
        obj.ModifyContents(newContents);

        // Assert
        obj.Contents.Should().HaveCount(1);
        obj.Contents[0].Content.Should().Be("Page 1");
    }

    [Fact]
    public void SetContents_UpdatesPages_WhenContentIsDifferent()
    {
        var editedAt = new DateTime(2002, 2, 3);

        // Arrange
        var obj = CreateTestStory();
        obj.ModifyContents(new List<string>
        {
            "Old Page 1",
        });

        var newContents = new List<string> { "New Page 1" };

        // Act
        obj.ModifyContents(newContents);

        // Assert
        obj.Contents.Should().HaveCount(1);
        obj.Contents[0].Content.Should().Be("New Page 1");
    }

    [Fact]
    public void SetContents_DoesNotUpdate_WhenContentIsSame()
    {
        var editedAt = new DateTime(2002, 2, 3);

        // Arrange
        var obj = CreateTestStory();
        obj.ModifyContents(new List<string>
        {
            "Page 1",
        });

        var newContents = new List<string> { "Page 1" };

        // Act
        obj.ModifyContents(newContents);

        // Assert
        obj.Contents.Should().HaveCount(1);
        obj.Contents[0].Content.Should().Be("Page 1");
    }

    [Fact]
    public void SetContents_HandlesEmptyNewContents()
    {
        var editedAt = new DateTime(2002, 2, 3);

        // Arrange
        var obj = CreateTestStory();
        obj.ModifyContents(new List<string>
        {
            "Page 1",
        });

        var newContents = new List<string>();

        // Act
        obj.ModifyContents(newContents);

        // Assert
        obj.Contents.Should().BeEmpty();
    }
}
