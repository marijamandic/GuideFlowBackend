namespace Explorer.Stakeholders.API.Dtos.Problems;
public class ResolutionDto
{
    public DateTime ReportedAt { get; set; }
    public bool IsResolved { get; set; }
    public DateTime Deadline { get; set; }
}
