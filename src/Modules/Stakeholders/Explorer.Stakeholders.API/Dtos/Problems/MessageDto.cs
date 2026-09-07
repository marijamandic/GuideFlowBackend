namespace Explorer.Stakeholders.API.Dtos.Problems;
public class MessageDto
{
    public int Id { get; set; }
    public int ProblemId { get; set; }
    public int UserId { get; set; }
    public string Content { get; set; }
    public DateTime PostedAt { get; set; }
}
