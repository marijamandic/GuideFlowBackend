namespace Explorer.Stakeholders.API.Dtos.Problems;

public class DetailsDto
{
    public ProblemCategory Category { get; set; }
    public ProblemPriority Priority { get; set; }
    public string Description { get; set; }
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