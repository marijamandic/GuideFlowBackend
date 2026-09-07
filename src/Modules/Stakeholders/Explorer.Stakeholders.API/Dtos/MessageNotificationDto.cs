using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    namespace Explorer.Stakeholders.Core.DTO
    {
        public class MessageNotificationDto
        {
            public int Id { get; set; } 
            public string Message { get; set; }
            public bool IsBlog { get; set; }
            public bool IsOpened { get; set; }
            public DateTime CreatedAt { get; set; }
            public int ObjectId { get; set; }
            public int SenderId { get; set; }
            public string Sender { get; set; }
            public int UserId { get; set; }
        }
    }

}
