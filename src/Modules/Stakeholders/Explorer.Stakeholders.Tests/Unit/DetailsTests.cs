using Explorer.Stakeholders.Core.Domain.Problems;

namespace Explorer.Stakeholders.Tests.Unit;

public class DetailsTests
{
    [Theory]
    [InlineData(ProblemCategory.Accommodation, ProblemPriority.High, "soba katastrofa")]
    [InlineData(ProblemCategory.Organization, ProblemPriority.Low, "ne svidja mi se plan")]
    [InlineData(ProblemCategory.Guides, ProblemPriority.Medium, "vodic slabo razumljiv")]
    public void Constructor_WithValidArguments_ShouldCreateInstance (
        ProblemCategory category,
        ProblemPriority priority,
        string description)
    {
        // Act
        var details = new Details(category, priority, description);

        // Assert
        Assert.Equal(category, details.Category);
        Assert.Equal(priority, details.Priority);
        Assert.Equal(description, details.Description);
    }

    [Theory]
    [InlineData((ProblemCategory)100, ProblemPriority.High, "soba katastrofa")]
    [InlineData(ProblemCategory.Organization, (ProblemPriority)100, "ne svidja mi se plan")]
    [InlineData(ProblemCategory.Organization, ProblemPriority.Low, " ")]
    public void Constructor_WithInvalidArguments_ShouldThrowArgumentException(
        ProblemCategory category,
        ProblemPriority priority,
        string description)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Details(category, priority, description));
    }
}
