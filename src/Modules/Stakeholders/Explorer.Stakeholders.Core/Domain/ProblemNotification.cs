namespace Explorer.Stakeholders.Core.Domain;

public class ProblemNotification : Notification
{
    public long ProblemId { get; private set; }
    public ProblemNotificationType Pnt { get; private set; }

    public ProblemNotification(
        long userId,
        string sender,
        string message,
        DateTime createdAt,
        bool isOpened,
        NotificationType type,
        long problemId,
        ProblemNotificationType pnt) : base(userId, sender, message, createdAt, isOpened, type)
    {
        ProblemId = problemId;
        Pnt = pnt;
    }

    public enum ProblemNotificationType
    {
        NewMessage,
        NewDeadline,
        DeadlinePassed
    }
}