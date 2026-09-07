using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos.Chatbot
{
    public  class ChatMessageDto
    {
        public string Content { get; set; }
        public Sender Sender { get; set; }
    }

    public enum Sender
    {
        Chatbot,
        User
    }
}
