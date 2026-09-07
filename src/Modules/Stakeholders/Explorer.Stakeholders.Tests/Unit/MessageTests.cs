using Explorer.Stakeholders.Core.Domain.Problems;

namespace Explorer.Stakeholders.Tests.Unit;

public class MessageTests
{
    [Theory]
    [InlineData("poruka 1")]
    [InlineData("poruka 2")]
    public void Constructor_WithValidArguments_ShouldCreateInstance (string content)
    {
        // Arrange
        long problemId = 1;
        long userId = 1;
        DateTime postedAt = DateTime.UtcNow;

        // Act
        var message = new Message(problemId, userId, content, postedAt);

        // Assert
        Assert.Equal(problemId, message.ProblemId);
        Assert.Equal(userId, message.UserId);
        Assert.Equal(content, message.Content);
        Assert.Equal(postedAt, message.PostedAt);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData(null)]
    public void Constructor_WithInvalidArguments_ShouldThrowArgumentException(string content)
    {
        // Arrange
        long problemId = 1;
        long userId = 1;
        DateTime postedAt = DateTime.UtcNow;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Message(problemId, userId, content, postedAt));
    }
}
