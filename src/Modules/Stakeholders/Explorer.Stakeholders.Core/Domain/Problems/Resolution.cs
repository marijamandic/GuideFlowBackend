using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;

namespace Explorer.Stakeholders.Core.Domain.Problems;

public class Resolution : ValueObject<Resolution>
{
    public DateTime ReportedAt { get; }
    public bool IsResolved { get; set; }
    public DateTime Deadline { get; }

    [JsonConstructor]
    public Resolution(DateTime reportedAt, bool isResolved, DateTime deadline)
    {
        ReportedAt = reportedAt;
        IsResolved = isResolved;
        Deadline = deadline;
        Validate();
    }

    private void Validate()
    {
        if (Deadline < ReportedAt.AddDays(5))
            throw new ArgumentException("Invalid deadline");
    }

    protected override bool EqualsCore(Resolution other)
    {
        return ReportedAt == other.ReportedAt && IsResolved == other.IsResolved && Deadline == other.Deadline;
    }

    protected override int GetHashCodeCore()
    {
        unchecked
        {
            int hashCode = ReportedAt.GetHashCode();
            hashCode = (hashCode * 397) ^ IsResolved.GetHashCode();
            hashCode = (hashCode * 397) ^ Deadline.GetHashCode();
            return hashCode;
        }
    }
    public void ChangeResolveStatus(bool status)
    {
        IsResolved = status;
    }
}