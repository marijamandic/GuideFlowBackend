using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain;

public class Notification : Entity
{
    public long UserId { get; protected set; }
    public string Sender { get; protected set; }
    public string Message { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public bool IsOpened { get; protected set; }
    public NotificationType Type { get; protected set; }

    public Notification (long userId, string sender, string message, DateTime createdAt, bool isOpened, NotificationType type)
    {
        UserId = userId;
        Sender = sender;
        Message = message;
        CreatedAt = createdAt;
        IsOpened = isOpened;
        Type = type;
        Validate();
    }

    public Notification() { }
    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Sender)) throw new ArgumentException("Invalid Sender");
        else if (string.IsNullOrWhiteSpace(Message)) throw new ArgumentException("Invalid Message");
        else if (Type != NotificationType.ProblemNotification && Type != NotificationType.MoneyExchange && Type != NotificationType.ClubNotification && Type != NotificationType.MessageNotification) throw new ArgumentException("Invalid Notification Type");
    }

    public void UpdateIsOpened(bool isOpened)
    {
        IsOpened = isOpened;
    }
}

public enum NotificationType
{
    ProblemNotification,
    MoneyExchange,
    ClubNotification,
    MessageNotification
}