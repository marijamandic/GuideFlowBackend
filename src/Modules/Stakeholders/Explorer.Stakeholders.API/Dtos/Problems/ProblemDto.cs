namespace Explorer.Stakeholders.API.Dtos.Problems;
public class ProblemDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TourId { get; set; }
    public ProblemCategory Category { get; set; }
    public ProblemPriority Priority { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateOnly ReportedAt { get; set; }
    public ResolutionDto Resolution { get; set; } = new ResolutionDto { IsResolved = false, Deadline = DateTime.Today.AddDays(5) };
    public List<MessageDto> Messages { get; set; } = new List<MessageDto>();
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