using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;

namespace Explorer.Stakeholders.Core.Domain.Problems;

public class Details : ValueObject<Details>
{
    public ProblemCategory Category { get; }
    public ProblemPriority Priority { get; }
    public string Description { get; }

    [JsonConstructor]
    public Details(ProblemCategory category, ProblemPriority priority, string description)
    {
        Category = category;
        Priority = priority;
        Description = description;
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

    protected override bool EqualsCore(Details other)
    {
        return Category == other.Category &&
            Priority == other.Priority &&
            Description == other.Description;
    }

    protected override int GetHashCodeCore()
    {
        unchecked
        {
            int hashCode = Category.GetHashCode();
            hashCode = (hashCode * 397) ^ Priority.GetHashCode();
            hashCode = (hashCode * 397) ^ Description.GetHashCode();
            return hashCode;
        }
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