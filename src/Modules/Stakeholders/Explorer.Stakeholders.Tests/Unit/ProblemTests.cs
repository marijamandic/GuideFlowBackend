using Explorer.Stakeholders.Core.Domain.Problems;

namespace Explorer.Stakeholders.Tests.Unit;
public class ProblemTests
{
    [Fact]
    public void Constructor_WithValidArguments_ShouldInitializeProblem()
    {
        // Arrange
        long id = 1;
        long userId = 1;
        long tourId = 2;

        // Act
        var problem = new Problem(id, userId, tourId);

        // Assert
        Assert.Equal(userId, problem.UserId);
        Assert.Equal(tourId, problem.TourId);
        Assert.Empty(problem.Messages);
    }

    [Fact]
    public void AddMessage_WithValidMessage_ShouldAddMessageToList()
    {
        // Arrange
        var problem = new Problem(1, 1, 2);
        var message = new Message(1, 1, "Test message", DateTime.Now);

        // Act
        problem.AddMessage(message);

        // Assert
        Assert.Single(problem.Messages);
        Assert.Equal(message, problem.Messages[0]);
    }

    [Fact]
    public void AddMessage_WithInvalidMessage_ShouldThrowException()
    {
        // Arrange
        var problem = new Problem(1, 1, 2);
        var invalidMessage = new Message(2, 1, "poruka", DateTime.Now); // problem id mismatch, problem.Id = 1, message.ProblemId = 2

        // Act & Assert
        Assert.Throws<Exception>(() => problem.AddMessage(invalidMessage));
    }
}
