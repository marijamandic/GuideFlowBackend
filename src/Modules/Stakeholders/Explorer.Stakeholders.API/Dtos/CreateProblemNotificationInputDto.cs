namespace Explorer.Stakeholders.API.Dtos;

public class CreateProblemNotificationInputDto
{
    public int UserId { get; set; }
    public string Sender { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public int ProblemId { get; set; }
    public ProblemNotificationType Pnt { get; set; }
}
