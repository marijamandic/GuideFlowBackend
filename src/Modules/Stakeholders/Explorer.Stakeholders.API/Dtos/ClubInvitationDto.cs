using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class ClubInvitationDto
    {
        public long ClubId { get; set; }  
        public long TouristID { get; set; }  
        public string Status { get; set; } // Nek ostane string za sada. Videćemo posle kog tipa da bude

    }
}
