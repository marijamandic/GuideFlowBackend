namespace Explorer.Stakeholders.Core.Domain;

public class ProblemNotification : Notification
{
    public long ProblemId { get; private set; }
    public bool IsNewDeadline { get; private set; }

    public ProblemNotification(
        long userId,
        string sender,
        string message,
        DateTime createdAt,
        bool isOpened,
        NotificationType type,
        long problemId,
        bool isNewDeadline) : base(userId, sender, message, createdAt, isOpened, type)
    {
        ProblemId = problemId;
        IsNewDeadline = isNewDeadline;
    }
}