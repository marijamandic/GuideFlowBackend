namespace Explorer.Stakeholders.API.Dtos.Problems;

public class ProblemDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TourId { get; set; }
    public DetailsDto Details { get; set; }
    public ResolutionDto Resolution { get; set; }
    public List<MessageDto> Messages { get; set; } = new List<MessageDto>();
}