using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos.Club
{
    public class ClubDto
    {
        public int Id { get; set; }
        public long OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageBase64 { get; set; }
    }
}
