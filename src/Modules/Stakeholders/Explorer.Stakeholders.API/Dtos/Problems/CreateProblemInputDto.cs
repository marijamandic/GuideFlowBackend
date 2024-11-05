namespace Explorer.Stakeholders.API.Dtos.Problems;

public class CreateProblemInputDto
{
    public int UserId { get; set; }
    public int TourId { get; set; }
    public ProblemCategory Category { get; set; }
    public ProblemPriority Priority { get; set; }
    public string Description { get; set; } = string.Empty;
}
