using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.Chatbot
{
    public class ChatMessage
    {
       public string Content { get; private set; }
       public Sender Sender { get; private set; }

       public ChatMessage(string content, Sender sender) 
        {
            Content = content;
            Sender = sender;
        }
    }
    public enum Sender
    {
        Chatbot,
        User
    }
}
