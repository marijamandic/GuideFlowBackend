using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public class LocationEncounterDto : EncounterDto
    {
        public string ImageUrl { get; set; }
        public double ActivationRange { get; set; }
        public long CheckpointId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
