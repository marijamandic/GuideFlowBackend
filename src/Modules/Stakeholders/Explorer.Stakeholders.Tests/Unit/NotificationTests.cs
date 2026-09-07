using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Tests.Unit;

public class NotificationTests
{
    [Fact]
    public void Constructor_WithValidParameters_ShouldCreateInstance()
    {
        // Arrange
        var userId = 1;
        var sender = "Autor";
        var message = "Message";
        var createdAt = DateTime.UtcNow;
        var isOpened = false;
        var type = NotificationType.ProblemNotification;

        // Act
        var notification = new Notification(userId, sender, message, createdAt, isOpened, type);

        // Assert
        Assert.Equal(userId, notification.UserId);
        Assert.Equal(sender, notification.Sender);
        Assert.Equal(message, notification.Message);
        Assert.Equal(isOpened, notification.IsOpened);
        Assert.Equal(type, notification.Type);
    }

    [Theory]
    [InlineData(null, "poruka 1", NotificationType.ProblemNotification)]
    [InlineData("autor", null, NotificationType.ProblemNotification)]
    [InlineData("turista", "poruka 2", (NotificationType)100)]
    public void Constructor_InvalidParameters_ShouldThrowArgumentException(string sender, string message, NotificationType type)
    {
        // Arrange
        var userId = 1;
        var createdAt = DateTime.UtcNow;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Notification(userId, sender, message, createdAt, false, type));
    }
}
