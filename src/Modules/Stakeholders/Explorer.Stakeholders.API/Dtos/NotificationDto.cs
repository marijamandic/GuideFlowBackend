using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class NotificationDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsOpened { get; set; }
        public NotificationType Type { get; set; }
    }
}
