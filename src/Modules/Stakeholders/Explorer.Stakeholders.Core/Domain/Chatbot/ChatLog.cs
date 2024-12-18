using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.Chatbot
{
    public class ChatLog : Entity
    {
        public long UserId { get; private set; }
        public List<ChatMessage> Messages { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public ChatLog(long userId)
        { 
            UserId = userId;
            Messages = new List<ChatMessage>();
        }
    }
}
