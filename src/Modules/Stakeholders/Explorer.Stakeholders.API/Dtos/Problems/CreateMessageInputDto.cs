namespace Explorer.Stakeholders.API.Dtos.Problems;

public class CreateMessageInputDto
{
    public int ProblemId { get; set; }
    public string Content { get; set; } = string.Empty;
}
