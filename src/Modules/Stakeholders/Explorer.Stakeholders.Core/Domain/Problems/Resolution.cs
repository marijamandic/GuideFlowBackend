using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;

namespace Explorer.Stakeholders.Core.Domain.Problems;

public class Resolution : ValueObject<Resolution>
{
    public bool IsResolved { get; }
    public DateTime Deadline { get; }

    [JsonConstructor]
    public Resolution(bool isResolved, DateTime deadline)
    {
        IsResolved = isResolved;
        Deadline = deadline;
        //Validate();
    }

    //Validate()

    protected override bool EqualsCore(Resolution other)
    {
        return IsResolved == other.IsResolved && Deadline == other.Deadline;
    }

    protected override int GetHashCodeCore()
    {
        unchecked
        {
            int hashCode = IsResolved.GetHashCode();
            hashCode = (hashCode * 397) ^ Deadline.GetHashCode();
            return hashCode;
        }
    }
}