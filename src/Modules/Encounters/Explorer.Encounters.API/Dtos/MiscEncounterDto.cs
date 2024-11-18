using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos
{
    public class MiscEncounterDto : EncounterDto
    {
        public string ActionDescription { get; set; }
    }
}
