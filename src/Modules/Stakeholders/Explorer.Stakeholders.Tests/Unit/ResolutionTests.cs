using Explorer.Stakeholders.Core.Domain.Problems;

namespace Explorer.Stakeholders.Tests.Unit;

public class ResolutionTests
{
    [Fact]
    public void Constructor_ShouldThrowArgumentException_IfDeadlineIsLessThenFiveDaysAfterReportedAt()
    {
        // Arrange
        var reportedAt = DateTime.UtcNow;
        var isResolved = false;
        var invalidDeadline = DateTime.UtcNow.AddDays(4);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Resolution(reportedAt, isResolved, invalidDeadline));
    }

    [Fact]
    public void Constructor_ShouldSetProperties_WhenDeadlineIsValid()
    {
        // Arrange
        var reportedAt = DateTime.UtcNow;
        var isResolved = false;
        var validDeadline = DateTime.UtcNow.AddDays(5);

        // Act
        var resolution = new Resolution(reportedAt, isResolved, validDeadline);

        // Assert
        Assert.Equal(reportedAt, resolution.ReportedAt);
        Assert.Equal(isResolved, resolution.IsResolved);
        Assert.Equal(validDeadline, resolution.Deadline);
    }
}
