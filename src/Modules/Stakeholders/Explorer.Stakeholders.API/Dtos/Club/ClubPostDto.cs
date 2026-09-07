using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos.Club
{
    public class ClubPostDto
    {
        public long Id { get; set; }
        public long ClubId { get; set; }
        public long MemberId { get; set; }
        public string Content { get; set; }
        public long? ResourceId { get; set; }
        public ResourceType? ResourceType { get; set; }
    }
}
