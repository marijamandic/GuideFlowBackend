namespace Explorer.Stakeholders.API.Dtos;

public class ProblemNotificationDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Sender { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsOpened { get; set; }
    public NotificationType Type { get; set; } = 0;
    public int ProblemId { get; set; }
    public ProblemNotificationType Pnt { get; set; }
}

public enum NotificationType
{
    ProblemNotification
}

public enum ProblemNotificationType
{
    NewMessage,
    NewDeadline,
    DeadlinePassed
}