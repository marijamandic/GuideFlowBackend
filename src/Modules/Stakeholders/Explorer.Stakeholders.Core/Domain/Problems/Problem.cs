using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain.Problems;

public class Problem : Entity
{
    private readonly List<Message> _messages = new();

    public long UserId { get; init; }
    public long TourId { get; init; }
    public ProblemCategory Category { get; private set; }
    public ProblemPriority Priority { get; private set; }
    public string Description { get; private set; }
    public DateOnly ReportedAt { get; private set; }
    public Resolution Resolution { get; private set; }
    public IReadOnlyList<Message> Messages => new List<Message>(_messages);

    public Problem(long userId, long tourId, ProblemCategory category, ProblemPriority priority, string description, DateOnly reportedAt)
    {
        UserId = userId;
        TourId = tourId;
        Category = category;
        Priority = priority;
        Description = description;
        ReportedAt = reportedAt;
        Validate();
    }

    private void Validate()
    {
        if (Category != ProblemCategory.Accommodation &&
            Category != ProblemCategory.Transportation &&
            Category != ProblemCategory.Guides &&
            Category != ProblemCategory.Organization &&
            Category != ProblemCategory.Safety) throw new ArgumentException("Invalid category");
        if (Priority != ProblemPriority.High && Priority != ProblemPriority.Medium && Priority != ProblemPriority.Low) throw new ArgumentException("Invalid priority");
        if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid description");
    }
}

public enum ProblemCategory
{
    Accommodation,
    Transportation,
    Guides,
    Organization,
    Safety
}

public enum ProblemPriority
{
    High,
    Medium,
    Low
}