using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public class SocialEncounterDto : EncounterDto
    {
        public int TouristNumber { get; set; }
        public double EncounterRange { get; set; }
    }
}
