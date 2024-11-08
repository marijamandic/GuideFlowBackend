using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class PublicPointNotificationDto
    {
        public int Id { get; set; }
        public int PublicPointId { get; set; }
        public int AuthorId { get; set; }
        public bool IsAccepted { get; set; }
        public string Comment { get; set; }
        public bool IsRead { get; set; }

    }
}
