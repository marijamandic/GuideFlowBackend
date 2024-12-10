using Explorer.BuildingBlocks.Core.Domain;
using System;

namespace Explorer.Stakeholders.Core.Domain
{
    public class MessageNotification : Entity
    {
        public string Message { get; private set; }
        public bool IsBlog { get; private set; }
        public bool IsOpened { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public int ObjectId { get; private set; }
        public int SenderId { get; private set; }
        public string Sender { get; private set; }
        public int UserId { get; private set; }

        public MessageNotification(string message, bool isBlog, bool isOpened, DateTime createdAt, int objectId, int senderId, string sender, int userId)
        {
            Message = message;
            IsBlog = isBlog;
            IsOpened = isOpened;
            CreatedAt = createdAt;
            ObjectId = objectId;
            SenderId = senderId;
            Sender = sender;
            UserId = userId;

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Message))
                throw new ArgumentException("Message cannot be null or empty.");
            if (Message.Length > 280)
                throw new ArgumentException("Message cannot exceed 280 characters.");
            if (ObjectId <= 0)
                throw new ArgumentException("ObjectId must be a positive number.");
            if (SenderId <= 0)
                throw new ArgumentException("SenderId must be a positive number.");
            if (UserId <= 0)
                throw new ArgumentException("UserId must be a positive number.");
        }
        public void SetIsOpened(bool isOpened) { 
            IsOpened = isOpened;
        }
    }
}