using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public class HiddenLocationEncounterDto : EncounterDto
    {
        public string? ImageUrl { get; set; }
        public double ActivationRange { get; set; }
        public long CheckpointId { get; set; }
        public double ImageLongitude { get; set; }
        public double ImageLatitude { get; set; }
        public string? ImageBase64 { get; set; }
    }
}
