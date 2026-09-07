using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos.Chatbot
{
    public class ChatLogDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public List<ChatMessageDto> Messages { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
